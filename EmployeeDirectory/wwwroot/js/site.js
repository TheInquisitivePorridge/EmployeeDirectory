//Этот код конечно работает как "живой" поиск, но он не совместим с системой страниц
//$("#search").on("keyup", function () {
//    var value = $(this).val();

//    $("table tr").each(function (index) {
//        if (index !== 0) {

//            $row = $(this);

//            var id = $.map($row.find('td'), function (element) {
//                return $(element).text()
//            }).join(' ');


//            if (id.indexOf(value) < 0) {
//                $row.hide();
//            }
//            else {
//                $row.show();
//            }
//        }
//    });
//});