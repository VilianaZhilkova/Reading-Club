$(function () {
    var discussionId = $("#discussionId").text();
    var chatHub = $.connection.chatHub;

    $.connection.hub.start().done(function () {
        var room = discussionId;
        chatHub.server.joinVisitor(room);
    });

    chatHub.client.addNewCommentToPage = function (content, username) {
        $("#comments").append("<p><span id=comment-author><strong>" + username + ": </strong></span><span id=comment-content>" + content + "</span></p>");
    };

    $("#discussion-buttons").on("submit", function (ev) {
        ev.preventDefault();
        var content = $("#comment-content").val();
        console.log($("#comment-content").val());
        chatHub.server.addComment(content, discussionId);
        $("#comment-content").val("");
    });
});