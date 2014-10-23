var user_id = null;
var offset = 0;
var offsetArray = new Array();
var friends = null;
var counter=0;
function initializeVk() {
    console.log("Page loaded");
    VK.init({
        apiId: 4575060
    });
    VK.Auth.getLoginStatus(authInfo);
    VK.UI.button('login_button');
    VK.Auth.login(authInfo,2);
}
function authInfo(response) {
    if (response.session) {
        //Hiding vk auth button
        $("#login_button").hide();
        $("#demo-content").removeClass("hidden");
        user_id = response.session.mid;
        VK.Api.call('users.get', { uids: response.session.mid, fields: "photo_200_orig,sex" }, function (r) {
            if (r.response) {
                var user = r.response[0];
                if (!isAdded(user.uid))
                    addNode(user);
                showUserInfo(user);
            }
        });
    } else {
        console.log('not auth');
    }
}

function getFriends() {
    startSpinner();
    var time = 0;
    //Getting user friends
    if (user_id != null) {
        if (checkHasNoFriends(user_id)) {
            if (!offsetArray[user_id])
            {
                offsetArray[user_id] = 0;
            }
            VK.Api.call('friends.get', { user_id: user_id, fields: "photo_200_orig,sex,counters", offset: offsetArray[user_id], count : 5 }, function (r) {
                counter=0;
                if (r.response) {
                    offsetArray[user_id]+=5;
                    friends = new Array();
                    time = friends.length;
                    console.log(r.response);
                    for (var i=0;i<r.response.length;i++)
                    {
                        friends[i]=r.response[i];
                        VK.Api.call('friends.getMutual', {source_uid: user_id, target_uid:friends[i].uid}, function (resp){
                            if (resp.response)
                            {
                                console.log(friends[counter]);
                                if (!isAdded(friends[counter].uid)){
                                    addNode(friends[counter]);
                                }
                                addEdge(user_id, friends[counter].uid, resp.response.length);
                                counter++;
                            }
                        });

                    }
                    //Value - вес ребра
                }
                stopSpinner(time);
            }, function () { stopSpinner(time); });
        } else {
            console.log('already has friends');
        }
    }
    else {
        console.log('not auth');
    }

}