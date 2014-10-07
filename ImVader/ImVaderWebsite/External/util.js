function showUserInfo(user) {
    $("#user_name").text(user.first_name + " " + user.last_name);
    $("#user_pic").html("<img alt='User Pic' src=" + user.photo_200_orig + " class='img-rounded'>");
    $("#user_id").text(user.uid);
    if (user.sex == 1) {
        $("#user_gender").text("Female");
    } else
        $("#user_gender").text("Male");
}

function onPageLoaded() {
    initializeVk();
    initializeGraph();
    initializeSpinner();
}