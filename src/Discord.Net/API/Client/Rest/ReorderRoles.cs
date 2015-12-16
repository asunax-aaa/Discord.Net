﻿using Discord.API.Converters;
using Newtonsoft.Json;
using System.Linq;

namespace Discord.API.Client.Rest
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class ReorderRolesRequest : IRestRequest<Role[]>
    {
        string IRestRequest.Method => "PATCH";
        string IRestRequest.Endpoint => $"guilds/{GuildId}/roles";
        object IRestRequest.Payload
        {
            get
            {
                int pos = StartPos;
                return RoleIds.Select(x => new Role(x, pos++));
            }
        }
        bool IRestRequest.IsPrivate => false;

        public sealed class Role
        {
            [JsonProperty("id"), JsonConverter(typeof(LongStringConverter))]
            public ulong Id { get; }
            [JsonProperty("position")]
            public int Position { get; }

            public Role(ulong id, int pos)
            {
                Id = id;
                Position = pos;
            }
        }
        
        public ulong GuildId { get; }

        public ulong[] RoleIds { get; set; } = new ulong[0];
        public int StartPos { get; set; } = 0;

        public ReorderRolesRequest(ulong guildId)
        {
            GuildId = guildId;
        }
    }
}
