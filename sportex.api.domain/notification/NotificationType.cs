using System;
using System.Collections.Generic;
using System.Text;

namespace sportex.api.domain.notification
{
    public enum NotificationType
    {
        DEFAULT,
        UPDATE,
        REMINDER,
        EVENT_DATA_CHANGED,
        EVENT_CANCELATION,
        EVENT_PARTICIPANT_JOINED,
        EVENT_PARTICIPANT_DROPED,
        PLAYER_STARTER,
        PLAYER_REVIEWED,
        EVENT_INVITATION
    }
}