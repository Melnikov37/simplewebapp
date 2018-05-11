function LoadBlogs() {
    var myObj, i, j, x = "";
    var request = new XMLHttpRequest();
    request.open("GET", "/api/blogsapi/", false);
    request.send();

    myObj = JSON.parse(request.responseText);
    var x = "";
    for (i in myObj) {
        x += "<hr>"
        x += "<h4> Блог номер " + myObj[i].blogId + " : <a href='" + myObj[i].url + "'>" + myObj[i].url + "</a></h4>";
        x += "<button type='button' class='btn btn-sm btn-outline-secondary' onclick='DeleteBlog(" + myObj[i].blogId + ");'>Удалить</button>"

        for (j in myObj[i].post) {
            x += "<div class='col-10'>";
            x += "<h4>" + myObj[i].post[j].title + "</h4>";
            x += "<p>December 23, 2017 by <a href='#'>Ivan</a></p>";
            x += "<p>" + myObj[i].post[j].content + "</p><br>";
            x += "</div>";
        }
    }
    document.getElementById("blogsDiv").innerHTML = x;
}

function DeleteBlog(id) {
    var request = new XMLHttpRequest();
    select = document.getElementById("deleteDiv");
    url = "/api/blogsapi/" + id;
    request.open("DELETE", url, false);
    request.send();
    location.reload();
}

function CreateBlog() {
    urlText = document.getElementById("createDiv").value;
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("POST", "/api/blogsapi/");
    xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    xmlhttp.send(JSON.stringify({ url: urlText }));
}