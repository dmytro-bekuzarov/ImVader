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
    loadGraph(files);
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
}

function onPageLoaded() {
    initializeEvents();
    initializeVk();
    initializeGraph();
    initializeSpinner();
}


///////////shortest path
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
    if (shortestPathNodeListIndexes != 0)
        network.selectNodes(shortestPathNodeListIndexes);
    else {
        network.selectNodes([]);
    }
}

function goToServer() {
    console.log(shortestPathNodeListIndexes);
    var edges = {};

    $.getJSON('/api/ShortestPath', { vertices: shortestPathNodeListIndexes, edges: edges }, function(data) {
        
    });
}