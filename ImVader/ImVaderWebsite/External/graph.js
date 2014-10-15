var nodes = new vis.DataSet();
var edges = new vis.DataSet();
var textFile = null;

var data = {
    nodes: nodes,
    edges: edges
};
var options = {
    width: '100%',
    height: '100%',
    selectable:false
};
var container;
var network;

function loadGraph(files) {
    var reader = new FileReader();
    reader.readAsText(files[0]);
    reader.onload = function (e) {
        var text = reader.result;
        var graph = jQuery.parseJSON(reader.result);
        startSpinner();
        clearGraph();

        var initialNode = graph.nodes[0];
        user_id = initialNode.uid;
        showUserInfo(initialNode);

        for (var j = 0; j < graph.nodes.length; j++) {
            addNode(graph.nodes[j]);
        }
        for (var j = 0; j < graph.edges.length; j++) {
            addEdge(graph.edges[j].from, graph.edges[j].to);
        }
        stopSpinner(graph.edges.length);
    }
}

function makeTextFile(text) {
    var data = new Blob([text], { type: 'text/javascript' });

    if (textFile !== null) {
        window.URL.revokeObjectURL(textFile);
    }

    textFile = window.URL.createObjectURL(data);

    return textFile;
};

function selectNode(properties) {
    if (properties.nodes != null && properties.nodes.length != 0 && properties.nodes[0] != undefined) {
        var selectedNode = nodes._data[properties.nodes[0]];
        user_id = selectedNode.uid;
        showUserInfo(selectedNode);
    }
}

function initializeGraph() {
    container = document.getElementById('graph_place');
    network = new vis.Network(container, data, options);

    network.on('select', selectNode);
}

function addNode(user) {
    user.id = user.uid;
    user.label = user.first_name;
    if (user.sex == 1) user.color = {
        background: 'red',
        highlight: {
            border: 'black'
        }
    }
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

function clearGraph() {
    var nodez = Object.keys(nodes._data).map(function (k) {
        return nodes._data[k];
    });
    var edgez = Object.keys(edges._data).map(function (k) {
        return edges._data[k];
    });
    for (var i = 0; i < nodez.length; i++) {
        nodes.remove(nodez[i].uid);
    }
    for (var i = 0; i < edgez.length; i++) {
        nodes.remove(edgez[i].id);
    }
}

function getGraphAsJson() {
    var graph = {};
    graph.edges = new Array();
    graph.nodes = new Array();
    var nodez = Object.keys(nodes._data).map(function (k) {
        return nodes._data[k];
    });
    var edgez = Object.keys(edges._data).map(function (k) {
        return edges._data[k];
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