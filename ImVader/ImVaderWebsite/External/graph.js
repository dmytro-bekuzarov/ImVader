var nodes = new vis.DataSet();
var edges = new vis.DataSet();
var textFile = null;

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
            addEdge(graph.edges[j].from, graph.edges[j].to, graph.edges[j].Weight);
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

function highlightComponents(components) {
    var colors = [];

    colors.push("#8A2BE2");
    colors.push("#DC143C");
    colors.push("#FFD700");
    colors.push("#FF4500");
    colors.push("#7FFF00");
    colors.push("#00CED1");
    colors.push("#F0E68C");

    var counter = 0;
    for (var i = 0; i < components.length; i++) {
        counter %= 7;
        if (components[i].length == 1) {
            data.nodes._data[components[i][0]].color = { background: "#2F4F4F" }
        } else {
            for (var j = 0; j < components[i].length; j++) {
                if (components[i][j] != null && components[i][j] != undefined)
                    data.nodes._data[components[i][j]].color = { background: colors[counter] };
                for (var k in data.edges._data) {
                    if ((edges._data[k].from == components[i][j] && components[i].indexOf(edges._data[k].to) != -1) ||
                        (edges._data[k].to == components[i][j] && components[i].indexOf(edges._data[k].from) != -1)) {
                        edges._data[k].color = colors[counter];
                    }
                }
            }
            counter++;
        }
    }
    network.setData(data);
}

function hightlightEdgesByFromTo(recievedEdges, nodesArray) {
    setDefaultVisualOptions();
    for (var i = 0; i < recievedEdges.length; i++) {
        for (var edgeId in data.edges._data) {
            if (data.edges._data[edgeId].from == nodesArray[recievedEdges[i].From].uid &&
                data.edges._data[edgeId].to == nodesArray[recievedEdges[i].To].uid) {
                data.edges._data[edgeId].color = '#FFCC33';
            }
        }
    }
    network.setData(data);
}

function higlightPath(selectedNodes) {
    setDefaultVisualOptions();
    for (var i = 0; i < selectedNodes.length; i++) {
        data.nodes._data[selectedNodes[i]].color = {
            background: '#99FF99',
            border: '#006600',
            highlight: {
                background: 'lightgreen',
                border: 'darkgreen'
            }
        }
        for (var id in data.edges._data) {
            //    console.log(data.edges._data[id].color);
        }
        for (var id in data.edges._data) {
            for (var k = 0; k < selectedNodes.length; k++) {
                if (data.edges._data[id].from == selectedNodes[i] &&
                    data.edges._data[id].to == selectedNodes[k]) {
                    data.edges._data[id].color = '#33CC66';
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
            }
        }
    }
    network.setData(data);
}

function setDefaultVisualOptions() {
    for (var id in data.edges._data) {
        data.edges._data[id].color = '#bce8f1';
    }
    for (var id in data.nodes._data) {
        data.nodes._data[id].allowedToMoveX = true;
        data.nodes._data[id].allowedToMoveY = true;
        if (data.nodes._data[id].sex == 1) {
            data.nodes._data[id].color = {
                border: '#CC0033',
                background: 'pink'
            };
        }
        else {
            data.nodes._data[id].color = {
                border: '#5295EF',
                background: '#99C2FC'
            };
        }
    }
    network.setData(data);
}

function addNode(user) {
    user.id = user.uid;
    user.label = user.first_name;
    if (user.sex == 1) user.color = {
        background: 'pink',
        border: '#CC0033',
        highlight: {
            background: 'pink',
            border: 'red'
        }
    }

    nodes.add(user);
    return nodes;
}

function addEdge(from, to) {
    edges.add({ from: from, to: to });
    return edges;
}

function addEdge(from, to, value) {
    factor = value == 0 ? 0.5 : 1 / Math.sqrt(value);
    edges.add({
        from: from, to: to, value: value, label: value, color: '#bce8f1', arrowScaleFactor: factor, style: "arrow"
    });
}

function hasEdge(from, to) {
    for (var i = 0; i < edges._data.length; i++) {
        if ((edges._data[i].from == from && edges._data[i].to == to))// || (edges._data[i].to == from && edges._data[i].from == to))
            return true;
    }
    return false;
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
    //else return false;;;
    return true;
}

function clearNodes() {
    var nodez = Object.keys(nodes._data).map(function (k) {
        return nodes._data[k];
    });
    for (var i = 0; i < nodez.length; i++) {
        nodes.remove(nodez[i].uid);
    }
}

function clearEdges() {
    var edgez = Object.keys(edges._data).map(function (k) {
        return edges._data[k];
    });
    for (var i = 0; i < edgez.length; i++) {
        edges.remove(edgez[i].id);
    }
}

function clearGraph() {
    clearNodes();
    clearEdges();
}

function getEdges() {
    var localEdges = new Array();
    var edgez = Object.keys(edges._data).map(function (k) {
        return edges._data[k];
    });
    for (var i = 0; i < edgez.length; i++) {
        localEdges.push({
            from: parseInt(edgez[i].from),
            to: parseInt(edgez[i].to),
            Weight: parseInt(edgez[i].value)
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

function getNodesAsMap() {
    return jQuery.extend(true, {}, nodes._data);
}

function getCenterCoords() {
    return network.getCenterCoordinates();
}

function getGraphAsJson() {
    var graph = {};
    graph.edges = getEdges();
    graph.nodes = getNodes();

    return graph;
};