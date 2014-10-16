var nodes = new vis.DataSet();
var edges = new vis.DataSet();
var textFile = null;

var data = {
    nodes: nodes,
    edges: edges,
    color: {
        border: 'black',
        background: '#CCCCFF'
    }
};

var options = {
    width: '100%',
    height: '100%'
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

function higlightPath(selectedNodes) {
    newObject = jQuery.extend(true, {}, oldObject);
    for (var i = 0; i < selectedNodes.length; i++) {
        data.nodes._data[selectedNodes[i]].color = {
            background: '#99FF99',
            border: '#006600'
        }
        for (var id in data.edges._data) {
            for (var k = 0; k < selectedNodes.length; k++) {
                if (data.edges._data[id].from == selectedNodes[i] &&
                    data.edges._data[id].to == selectedNodes[k]) {
                    data.edges._data[id].color = '#33CC66';
                    data.edges._data[id].width = 8;
                }
            }
        }
    }
    network.setData(data);
}

function highlightSubgraph(selectedNodes, selectedEdges) {
    for (var i = 0; i < selectedNodes.length; i++) {
        data.nodes._data[selectedNodes[i]].color = {
            background: '#99FF99',
            border: '#006600'
        }        
    }
    for (var id in data.edges._data) {
        for (var k = 0; k < selectedEdges.length; k++) {
            if (data.edges._data[id].from == selectedEdges[i].from &&
                data.edges._data[id].to == selectedEdges[k].to ||
                data.edges._data[id].from == selectedEdges[i].to &&
                data.edges._data[id].to == selectedEdges[k].from) {
                data.edges._data[id].color = '#33CC66';
                data.edges._data[id].width = 8;
            }
        }
    }
    network.setData(data);
}

function setDefaultVisualOptions() {
    for (var id in data.edges._data) {
        data.edges._data[id].color = {
            border: 'grey'
        };
        data.edges._data[id].width = 1;
    }
    for (var id in data.nodes._data) {
        data.nodes._data[id].color = {
            border: '#9999FF',
            backgroud: '#CCCCFF'
        };
    }
    network.setData(data);
}

function addNode(user) {
    user.id = user.uid;
    user.label = user.first_name;
    if (user.sex == 1) user.color = {
        background: 'red',
        highlight: {
            border: 'red'
        }
    }
    
    nodes.add(user);
    return nodes;
}

function addEdge(from, to) {
    for (var ed in data.edges._data) {
        if (data.edges._data[ed].from == to && data.edges._data[ed].to == from)
            return edges;
    }
    edges.add({ from: from, to: to, color: 'grey' });
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
    graph.edges = getEdges();
    graph.nodes = getNodes();
    return graph;
};

function getEdges() {
    var localEdges = new Array();
    var edgez = Object.keys(edges._data).map(function (k) {
        return edges._data[k];
    });
    for (var i = 0; i < edgez.length; i++) {
        localEdges.push({
            from: parseInt(edgez[i].from),
            to: parseInt(edgez[i].to)
        });
    }
    return localEdges;
}

function getNodes() {
    var localNodes = new Array();
    var nodez = Object.keys(nodes._data).map(function (k) {
        return nodes._data[k];
    });
    for (var i = 0; i < nodez.length; i++) {
        localNodes.push(new Object({
            first_name: nodez[i].first_name,
            last_name: nodez[i].last_name,
            uid: nodez[i].uid,
            photo_200_orig: nodez[i].photo_200_orig,
            sex: nodez[i].sex
        }));
    }
    return localNodes;
}