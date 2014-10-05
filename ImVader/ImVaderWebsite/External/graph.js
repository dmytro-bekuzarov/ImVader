nodes = new vis.DataSet();
edges = new vis.DataSet();

var container = document.getElementById('graph_place');
var data = {
    nodes: nodes,
    edges: edges
};
var options = {
    width: '100%',
    height: '100%'
};
var network = new vis.Network(container, data, options);

network.on('select', function (properties) {
    if (properties.nodes != null && properties.nodes.length != 0 && properties.nodes[0] != undefined) {
        var selectedNode = nodes._data[properties.nodes[0]];
        user_id = selectedNode.uid;
        showUserInfo(selectedNode);
    }
});

function addNode(user) {
    user.id = user.uid;
    user.label = user.first_name;
    if (user.sex == 1)user.color = 'red';
    nodes.add(user);
}
function addEdge(from, to) {
    edges.add({from: from, to: to});
}

function addFriends(friends) {
    for (var i = 0; i < friends.length; i++) {
        friends[i].id = friends[i].uid;
        if (!isAdded(friends[i].id))
            addNode(friends[i]);
        addEdge(user_id, friends[i].user_id);
    }
}
function isAdded(user_id) {
    var selectedNode = nodes._data[user_id];
    if (selectedNode == null || selectedNode == undefined) {
        return false;
    }
    else return true;
}
function checkHasNoFriends(user_id) {
    var selectedNode = nodes._data[user_id];
    if (selectedNode != null && selectedNode != undefined && !selectedNode.gotFriends) {
        selectedNode.gotFriends = true;
        return true;
    }
    else return false;
}
function showUserInfo(user) {
    $("#user_name").text(user.first_name + " " + user.last_name);
    $("#user_pic").html("<img alt='User Pic' src=" + user.photo_200_orig + " class='img-rounded'>");
    $("#user_id").text(user.uid);
    if (user.sex == 1) {
        $("#user_gender").text("Female");
    } else
        $("#user_gender").text("Male");
}