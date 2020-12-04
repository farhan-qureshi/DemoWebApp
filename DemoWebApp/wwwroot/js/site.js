// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).on('click', ".js_subscribed", function () {
    var ischecked = $(this).is(':checked');
    var title = $(this).attr('title')

    if (!ischecked)
    {
        $.ajax({
            url: 'unsubscribe?title=' + title,
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            error: function () {
                alert("There was a problem");
            }
        });
    }
    else
    {
        $.ajax({
            url: 'subscribe?title=' + title,
            method: "post",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            error: function () {
                alert("There was a problem");
            }
        });
    }
}); 

