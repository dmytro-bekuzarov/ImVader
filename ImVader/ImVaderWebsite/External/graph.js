var nodes = new vis.DataSet();
var edges = new vis.DataSet();

var data = {
    nodes: nodes,
    edges: edges
};
var options = {
    width: '100%',
    height: '100%'
};
var container;
var network;

function clearGraph() {
    nodes = new vis.DataSet();
    edges = new vis.DataSet();
}

function initializeGraph() {
    container = document.getElementById('graph_place');
    network = new vis.Network(container, data, options);

    network.on('select', function (properties) {
        if (properties.nodes != null && properties.nodes.length != 0 && properties.nodes[0] != undefined) {
            var selectedNode = nodes._data[properties.nodes[0]];
            user_id = selectedNode.uid;
            showUserInfo(selectedNode);
        }
    });
}

function addNode(user) {
    user.id = user.uid;
    user.label = user.first_name;
    if (user.sex == 1) user.color = 'red';
    nodes.add(user);
    return nodes;
}

function addEdge(from, to) {
    edges.add({ from: from, to: to });
    return edges;
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

function getGraphAsJson() {
    var graph = {};
    graph.edges = new Array();
    graph.nodes = new Array();
    var nodez = Object.keys(nodes._data).map(function (k) {
        return nodes._data[k]
    });
    var edgez = Object.keys(edges._data).map(function (k) {
        return edges._data[k]
    });
    for (var i = 0; i < nodez.length; i++) {
        graph.nodes.push(new Object({
            first_name: nodez[i].first_name,
            last_name: nodez[i].last_name,
            uid: nodez[i].uid,
            photo_200_orig: nodez[i].photo_200_orig,
            sex: nodez[i].sex
        }));
    }
    for (var i = 0; i < edgez.length; i++) {
        graph.edges.push({
            from: parseInt(edgez[i].from),
            to: parseInt(edgez[i].to)
        });
    }
    return graph;
}