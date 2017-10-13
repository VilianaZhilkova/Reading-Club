$(function () {


    var discussionId = $("#discussionId").text();
    var participantsHub = $.connection.participantsHub;

    $.connection.hub.start().done(function () {
        var room = discussionId;
        participantsHub.server.joinVisitor(room);
    });

    participantsHub.client.checkForChanges = function () {
        participantsHub.server.updateParticipants(discussionId);
    };

    participantsHub.client.updateParticipantsList = function (participants) {
        $("#participants-list").empty();
        for (var i = 0; i < participants.length; i++) {
            $("#participants-list").append("<li id=" + participants[i].Id + ">" + participants[i].UserName + "</li>");
        }
        var usersCount = participants.length;
        $("#users-count").text(usersCount);
    };  
});