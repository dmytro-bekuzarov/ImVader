function showUserInfo(user) {
    $("#user_name").text(user.first_name + " " + user.last_name);
    $("#user_pic").html("<img alt='User Pic' src=" + user.photo_200_orig + " class='img-rounded'>");
    $("#user_id").text(user.uid);
    if (user.sex == 1) {
        $("#user_gender").text("Female");
    } else
        $("#user_gender").text("Male");
}

function handleDragOver(evt) {
    evt.stopPropagation();
    evt.preventDefault();
    evt.dataTransfer.dropEffect = 'copy';
}

function handleFileSelect(evt) {
    evt.stopPropagation();
    evt.preventDefault();
    var files = evt.dataTransfer.files;
    clearGraph();
    loadGraph(files);
}

function selectNode(properties) {
    if (properties.nodes != null && properties.nodes.length != 0 && properties.nodes[0] != undefined) {
        var selectedNode = nodes._data[properties.nodes[0]];
        user_id = selectedNode.uid;
        showUserInfo(selectedNode);
    }
}

function getEdgesByNodes(nodes) {
    var localEdges = new Array();
    var edgez = Object.keys(edges._data).map(function (k) {
        return edges._data[k];
    });
    for (var i = 0; i < edgez.length; i++) {
        var froma = -1, toz = -1;
        for (var j = 0; j < nodes.length; j++) {
            if (nodes[j].uid == edgez[i].from)
            { froma = j; }
            if (nodes[j].uid == edgez[i].to)
            { toz = j; }
        }

        localEdges.push({
            from: froma,
            to: toz
        });
    }
    return localEdges;
}

function initializeEvents() {
    var create = document.getElementById('downloadlink');

    create.addEventListener('click', function () {
        var link = document.getElementById('downloadlink');
        link.href = makeTextFile(JSON.stringify(getGraphAsJson()));
    }, false);

    var dropZone = document.getElementById('drop_zone');
    dropZone.addEventListener('dragover', handleDragOver, false);
    dropZone.addEventListener('drop', handleFileSelect, false);

    var mstbtn = document.getElementById('mstbtn');
    mstbtn.addEventListener('click', function () {
        getMST();
    }, false);

    var strongbtn = document.getElementById('strong');
    strongbtn.addEventListener('click', function () {
        getStrong();
    }, false);

}

function onPageLoaded() {
    initializeEvents();
    initializeVk();
    initializeGraph();
    initializeSpinner();
}


//shortest path
$(document).ready(function () {
    $("#find-shortest-path").on('click', function () {
        $("#find-shortest-path-tooltip").html("Select two vertices");
        $("#find-shortest-path-start").removeClass("hidden");
        removeSelectEvent();
        addClickEvent();
    });
    $("#find-shortest-path-start").on('click', function () {
        $("#find-shortest-path-tooltip").html("");
        $("#find-shortest-path-start").addClass("hidden");
        addSelectEvent();
        removeClickEvent();
        goToServer();
    });
});

function addSelectEvent() {
    network.on('select', selectNode);
}

function removeSelectEvent() {
    network.off('select', selectNode);
}

function addClickEvent() {
    network.on('click', addToShortestPathList);
}
function removeClickEvent() {
    network.off('click', addToShortestPathList);
}

var shortestPathNodeListIndexes = new Array();

function addToShortestPathList(properties) {
    if (properties.nodes != null && properties.nodes.length != 0 && properties.nodes[0] != undefined) {
        var selectedNode = nodes._data[properties.nodes[0]];
        var id = selectedNode.id;
        var arrIndex = $.inArray(id, shortestPathNodeListIndexes);

        if (shortestPathNodeListIndexes.length != 2) {
            if (arrIndex == -1) {
                shortestPathNodeListIndexes.push(id);
            } else {
                shortestPathNodeListIndexes.splice(arrIndex, 1);
            }
        } else {
            if (arrIndex != -1) {
                shortestPathNodeListIndexes.splice(arrIndex, 1);
            }
        }
    }
    selectedArgumentPathNodes();
}

function selectedArgumentPathNodes() {
    if (shortestPathNodeListIndexes != 0) {
        network.selectNodes(shortestPathNodeListIndexes);
    }
    else {
        network.selectNodes([]);
    }
}

function selectNodes(array) {
    network.selectNodes(array);
}

function goToServer() {
    if (shortestPathNodeListIndexes.length<2) {
        alert("Select 2 nodes");
        return;
    }

    startSpinner();
    var nodes = getNodes();
    var nodesIds = new Array();
    for (var i = 0; i < nodes.length; i++) {
        nodesIds.push(nodes[i].uid);
    }
    var edges = getEdges();

    $.ajax({
        type: "POST",
        data: JSON.stringify({
            Vertices: nodesIds,
            Edges: edges,
            Vertex1: shortestPathNodeListIndexes[0],
            Vertex2: shortestPathNodeListIndexes[1]
        }),
        url: "api/ShortestPath",
        contentType: "application/json",
        success: function (data) {
            if (data != null) {
                shortestPathNodeListIndexes = data;
                selectedArgumentPathNodes();
                higlightPath(shortestPathNodeListIndexes);
            } else {
                alert('No path');
                selectNodes([]);
            }
            shortestPathNodeListIndexes.splice(0, shortestPathNodeListIndexes.length);
            stopSpinner();
        }
    });

}

function getMST() {
    console.log("Graph must be weighted");
    return;
    startSpinner();
    var nodes = getNodes();
    var nodesIds = new Array();
    for (var i = 0; i < nodes.length; i++) {
        nodesIds.push(nodes[i].uid);
    }
    var edges = getEdges();
    var udges = new Array();
    for (var j = 0; j < edges.length; j++) {
        var edge = {};
        edge.From = edges[j].from;
        edge.To = edges[j].to;
        edge.Weight = edges[j].value;
        udges.push(edge);
    }
    $.ajax({
        type: "POST",
        data: JSON.stringify({
            Vertices: nodesIds,
            Edges: udges
        }),
        url: "api/MinimalSpanningTree",
        contentType: "application/json",
        success: function (data) {
            higlightPath(data);
            stopSpinner();
        }
    });
}

function getStrong() {
    startSpinner();
    var nodes = getNodes();
    var nodesIds = new Array();
    for (var i = 0; i < nodes.length; i++) {
        nodesIds.push(nodes[i].uid);
    }
    var edges = getEdgesByNodes(nodes);

    $.ajax({
        type: "POST",
        data: JSON.stringify({
            Vertices: nodesIds,
            Edges: edges
        }),
        url: "api/StrongComponents",
        contentType: "application/json",
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                for (var j = 0; j < data[i].length; j++) {
                    data[i][j] = nodes[data[i][j]].uid;
                }
            }
            highlightComponents(data);
            stopSpinner();
        }
    });
}


function getMinimumCuts() {
    startSpinner();
    var nodes = getNodes();
    var nodesIds = new Array();
    for (var i = 0; i < nodes.length; i++) {
        nodesIds.push(nodes[i].uid);
    }
    var edges = getEdges();

    $.ajax({
        type: "POST",
        data: JSON.stringify({
            Vertices: nodesIds,
            Edges: edges
        }),
        url: "api/MinimumCuts",
        contentType: "application/json",
        success: function (response) {
            console.log(response);
            for (var i = 0; i < response.length; i++) {
                data.nodes._data[response[i]].color = { background: "#00A86B" };
            }
            network.setData(data);

            stopSpinner();
        }
    });
}

///////////////////////////////
$(document).ready(function () {
    $("#topological").on('click', function () {
        topologicalSort();
    });
    $("#findMinCutButton").on('click', function() {
        getMinimumCuts();
    });
});

function topologicalSort() {
    startSpinner();
    var nodes = getNodes();
    var nodesIds = new Array();
    for (var i = 0; i < nodes.length; i++) {
        nodesIds.push(nodes[i].uid);
    }
    var edges = getEdges();
    console.log(nodesIds);
    console.log(getEdgesAsMap());
    $.ajax({
        type: "POST",
        data: JSON.stringify({
            Vertices: nodesIds,
            Edges: edges
        }),
        url: "api/Topological",
        contentType: "application/json",
        success: function (data) {
            if (data != null) {
                console.log(data);
                var coords = getCenterCoords();
                var nodesMap=getNodesAsMap();
                clearNodes();

                for (var j = 0; j < data.length; j++) {
                    nodesMap[data[j]].x = coords.x + j*140;
                    nodesMap[data[j]].y = coords.y;
                   
                    nodesMap[data[j]].allowedToMoveY = true;
                    addNode(nodesMap[data[j]]);
                }

            } else {
                alert('Graph has cycles');
            }
            stopSpinner();
        }
    });
}