/**
 * Created by JetBrains PhpStorm.
 * User: bebecap
 * Date: 03.10.14
 * Time: 20:44
 * To change this template use File | Settings | File Templates.
 */
function onPageLoaded() {
    console.log("Page loaded");
    VK.init({
        apiId: 4575060
    });
    VK.Auth.getLoginStatus(authInfo);
    VK.UI.button('login_button');
}
function authInfo(response) {
    if (response.session) {
        //Hiding vk auth button
        $("#login_button").addClass("hidden");
        $("#vk_greetings").removeClass("hidden");
        $("#vk_friends_json").removeClass("hidden");

        //Getting user info
        console.log('user: ' + response.session.mid);
        VK.Api.call('users.get', { uids: response.session.mid }, function (r) {
            if (r.response) {
                $("#vk_greetings").text("Hello, " + r.response[0].first_name + ". Here are your friend list in JSON.");
            }
        });
        //Getting user friends
        VK.Api.call('friends.get', { user_id: response.session.mid, fields: "photo_200_orig" }, function (r) {
            if (r.response) {
                console.log(r);
                $("#vk_friends_json").text(JSON.stringify(r.response));
            }
        });
    } else {
        console.log('not auth');
    }
}