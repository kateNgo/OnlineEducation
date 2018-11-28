function IDClick(str) {
    $("#SelectedLevel3Id").val(str);
    $("#ToPDFContent").attr("href", "/HelpOnline/Level3/ToPDFContent?id=" + str);
    //alert($("#SelectedLevel3Id").val());
}