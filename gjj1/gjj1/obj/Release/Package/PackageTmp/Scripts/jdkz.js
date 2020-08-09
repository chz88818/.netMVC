    var jss = "@(Session["permission"])";
$(function () {
    if (jss == "普通权限") {
        alert("你没有足够权限访问该页面");
    // alert(jss);
    $('#tj').hide();
}
})
