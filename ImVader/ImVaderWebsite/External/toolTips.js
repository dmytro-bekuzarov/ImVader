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
        'title': "Click here to build minimum spanning tree of the graph"
    });

    $('#find-shortest-path').tooltip({
        'placement': 'top',
        'title': "Select 2 vertices of the graph and run the algorithm"
    });

    $('#saveload').tooltip({
        'placement': 'right',
        'title': "Click here to save graph to .txt file or load the graph from .txt file"
    });

    $('#set-default').tooltip({
        'placement': 'right',
        'title': "Click here to restore graph to initial state"
    });

    $('#min-cuts').tooltip({
        'placement': 'right',
        'title': "Click here to find minimum cuts of the graph"
    });

    $('#top-sort').tooltip({
        'placement': 'right',
        'title': "Click here to sort graph topologically"
    });

    $('#strong-comp').tooltip({
        'placement': 'right',
        'title': "Click here to find strong components of the graph"
    });


    $("#myModal").modal("show");
});
