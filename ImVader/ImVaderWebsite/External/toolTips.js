$(document).ready(function () {
    $('#getFriendsButton').tooltip({
        'placement': 'right',
        'title': "Click here to show user's friends"
    });

    $('#dijkstra').tooltip({
        'placement': 'right',
        'title': "Click here to find path between friends"
    });

    $('#mst').tooltip({
        'placement': 'right',
        'title': "Click here to build mst of the graph"
    });

    $('#saveload').tooltip({
        'placement': 'right',
        'title': "Click here to save graph to .txt file or load the graph from .txt file"
    });

    $("#myModal").modal("show");
});
