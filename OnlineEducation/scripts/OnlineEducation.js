function IDClick(str) {
    $("#SelectedLevel3Id").val(str);
    $("#ToPDFContent").attr("href", "/HelpOnline/Level3/ToPDFContent?id=" + str);
    //alert($("#SelectedLevel3Id").val());
}
function printiFrame() {

    var frm = document.getElementById("the_iFrame").contentWindow;
    frm.focus();// focus on contentWindow is needed on some ie versions
    frm.print();
    return false;
}