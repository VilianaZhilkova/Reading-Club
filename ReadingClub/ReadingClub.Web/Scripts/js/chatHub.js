﻿$(function () {
    var discussionId = $("#discussionId").text();
    var chatHub = $.connection.chatHub;
    chatHub.client.addNewCommentToPage = function (content, username) {
        $("#comments").append("<p><span id=comment-author><strong>" + username + ": </strong></span><span id=comment-content>" + content + "</span></p>");
    };

    $("#discussion-buttons").on("submit", function (ev) {
        ev.preventDefault();
        var content = $("#content").val();
        console.log(content);
        chatHub.server.addComment(content, discussionId);
        $("#content").val("");
    });

    $.connection.hub.start().done(function () {
        var room = discussionId;
        chatHub.server.joinVisitor(room);
    });
});