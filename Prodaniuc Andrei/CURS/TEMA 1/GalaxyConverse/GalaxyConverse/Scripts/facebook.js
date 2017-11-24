/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/facebook-js-sdk/facebook-js-sdk.d.ts" />
var FacebookAuth;
(function (FacebookAuth) {
    var button = $("#button");
    var messageBox = $("#messageBox");
    var logoutBtn = $("#logout");
    var permissionsBtn = $("#permissions");
    var token;
    function load() {
        FB.getLoginStatus(function (response) {
            if (response.status === "connected") {
                alert("Logged in");
            }
            else if (response.status === "not_authorized") {
                alert("not authorized");
                FB.login(function (response) {
                    this.token = response.authResponse.accessToken;
                }, { scope: 'user_managed_groups publish_actions' });
            }
            else {
                alert("not logged in" + response.status);
                FB.login(function (response) {
                    this.token = response.authResponse.accessToken;
                }, { scope: 'user_managed_groups publish_actions' });
            }
        });
        button.click(PostMessage);
        logoutBtn.click(Logout);
        permissionsBtn.click(GetPermissions);
    }
    FacebookAuth.load = load;
    function PostMessage() {
        var message = messageBox.val() + "\nPosted from AP application";
        FB.api('/2519852344907182/feed', 'post', { message: message }, function (response) {
            if (!response || response.error) {
                alert("error" + response.message);
            }
            else {
                alert(response.id);
            }
        });
    }
    function Logout() {
        FB.logout(function (response) {
            alert("Logged out!");
        });
    }
    function GetPermissions() {
        FB.api('me/permissions', 'get', function (response) {
            alert(response);
        });
    }
})(FacebookAuth || (FacebookAuth = {}));
function loadAuth() {
    FacebookAuth.load();
}
