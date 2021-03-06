using System;
using System.Collections.Generic;
using System.Threading;
using ElectronicVoting.Domain.Contract.Result;
using ElectronicVoting.Validator.Consensus;
using ElectronicVoting.Validator.Factory;
using ElectronicVoting.Validator.Interface;
using ElectronicVoting.Validator.MessageTask;
using ElectronicVoting.Validator.MessageTask.Serialization;
using ElectronicVoting.Validator.PriorityQueue;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.MixedReality.WebRTC;
using Newtonsoft.Json;

namespace ElectronicVoting.Validator
{
    public partial class ConnectionManagement :IConnectionManagement
    {
        public static class Factory
        {
            public static ConnectionManagement Create(HttpOrganizationAuthorizationResult authorization)
            {
                ConnectionManagement connectionManagement = new ConnectionManagement()
                {
                    Organization = authorization.Organization,
                    Blockchain = new Blockchain(),
                    _cancellationToken = new CancellationToken(),
                    _peers = new Dictionary<string, PeerConnection>(),
                    _priorityQueueAsync = new PriorityQueueAsync<NodePriorityQueue>(),
                };
                
                
                connectionManagement._priorityQueueAsync.ActionAutoReadNode += () =>
                {
                    Console.WriteLine("Wykonywanie Zadan");
                    while (!connectionManagement._priorityQueueAsync.IsEmpty())
                    {
                        var node = connectionManagement._priorityQueueAsync.Pop();
                        MessageBlock messageBlock = JsonConvert.DeserializeObject<MessageBlock>(node.Task);
                        TaskCommand taskCommand = new TaskCommand(connectionManagement, messageBlock);
                        taskCommand.Call();
                    }
                };
                
                connectionManagement._hubConnection = new HubConnectionBuilder().WithUrl(Routes.SignalR.Connection, options =>
                {
                    options.Headers.Add("Organization", authorization.Organization);
                    options.Headers.Add("OrganizationId", authorization.OrganizationId);
                    options.AccessTokenProvider = () => System.Threading.Tasks.Task.FromResult(authorization.Token);
                }).AddMessagePackProtocol().Build();
                
                connectionManagement._hubConnection.On("UpdateValidationServerList", async (string organization) =>
                {
                    try
                    {
                        Console.WriteLine("UpdateValidationServerList");
                        var configuration = CreateConfiguration(organization, connectionManagement);
                        var peerConnection = await PeerConnectionFactory.Create(configuration);
                        await peerConnection.AddDataChannelAsync("Low", false, true, connectionManagement._cancellationToken);
                        await peerConnection.AddDataChannelAsync("Normal", false, true, connectionManagement._cancellationToken);
                        await peerConnection.AddDataChannelAsync("High", true, true, connectionManagement._cancellationToken);
                        bool result = peerConnection.CreateOffer();
                        
                        connectionManagement._peers.Add(organization,peerConnection);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                });

                connectionManagement._hubConnection.On("SdpMessageReceivedConfigurationWebRtc", async (string organization, SdpMessage message) =>
                {
                    try
                    {
                        var isPeerConnection = connectionManagement._peers.ContainsKey(organization);
                        
                        if (!isPeerConnection)
                        {
                            var configuration = CreateConfiguration(organization, connectionManagement);
                            var peerConnection = await PeerConnectionFactory.Create(configuration);
                            await peerConnection.SetRemoteDescriptionAsync(message);
                            if (message.Type == SdpMessageType.Offer)
                            {
                                peerConnection.CreateAnswer();
                            }
                        
                            connectionManagement._peers.Add(organization,peerConnection);
                        }
                        else
                        {
                            var peerConnection = connectionManagement._peers[organization];
                            
                            await peerConnection.SetRemoteDescriptionAsync(message);
                            if (message.Type == SdpMessageType.Offer)
                            {
                                peerConnection.CreateAnswer();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    
                });

                connectionManagement._hubConnection.On("IceCandidateReceivedConfigurationWebRtc", (string organization, IceCandidate iceCandidate) =>
                {
                    Console.WriteLine("IceCandidateReceivedConfigurationWebRtc");
                    var peer = connectionManagement._peers[organization];
                    peer.AddIceCandidate(iceCandidate);
                });

                connectionManagement._hubConnection.On("ReceiveToSuperValidatorVoting", (string text) =>
                {
                    try
                    {
                        Console.WriteLine("ReceiveToSuperValidatorVoting");
                        MessageBlock messageBlock = new MessageBlock
                        {
                            Stage = Stage.PrePrepare,
                            Validators = connectionManagement.GetAllConnectionValidatorsName(),
                        };
                    
                        TaskCommand taskCommand = new TaskCommand(connectionManagement, messageBlock);
                        taskCommand.Call();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                });

                
                return connectionManagement;
            }

            private static ConnectionConfiguration CreateConfiguration(string organization,ConnectionManagement connectionManagement)
            {
                return new ConnectionConfiguration()
                {
                    DataChannelAddedDelegate = channel =>
                    {
                        channel.MessageReceived += bytes =>
                        {
                            Console.WriteLine("Odebrano nowa wiadomosc");
                            var obj  = SerializationTask.DeserializeEnvelope(bytes);
                            connectionManagement._priorityQueueAsync.Push(obj.Value,obj.Key);
                        };
                    },
                    SdpReadyToSend = async message =>
                    {
                        Console.WriteLine("Wysłanie konfiguracji SDP");
                        await connectionManagement._hubConnection.InvokeAsync("SdpMessageSendConfigurationWebRtc",organization, message);
                    },
                    IceCandidateReadyToSend = async candidate =>
                    {
                        Console.WriteLine("Wysłanie konfiguracji ICE");
                        await connectionManagement._hubConnection.InvokeAsync("IceCandidateSendConfigurationWebRtc",organization, candidate);
                    },
                };
            }
        }
    }
}