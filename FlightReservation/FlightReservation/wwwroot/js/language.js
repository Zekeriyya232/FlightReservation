$(document).ready(function () {
    var arrLang = {
        'tr': {
            '1': 'Kalk�� Havaliman� Se�iniz',
            '2': 'Var�� Havaliman� Se�iniz',
            '3': 'Kalk�� Tarihi',
            '4': 'U�u� Ara',
            '5': 'Kalk�� Tarihi',
            '6': 'Kalk��',
            '7': 'Var��',
            '8': 'Bilet Fiyat',
            '9': 'Bilet ��lemini Tamamla',
            '10': 'Kalk�� Tarihi',
            '11': 'D�zenle',
            '12': 'Kullan�c� Ad�',
            '13': 'Kullan�c� Soyad�',
            '14': 'Telefon',
            '15': 'Do�um Tarihi',
            
        },
        'en': {
            '1': 'Please Choose Departure Airport',
            '2': 'Please Choose Arrival Airport',
            '3': 'Departure Date',
            '4': 'Search Flight',
            '5': 'Departure Date',
            '6': 'Departure',
            '7': 'Arrivale',
            '8': 'Ticket Price',
            '9': 'Select Seat',
            '10': 'Departure Date',
            '11': 'Edit',
            '12': 'Name',
            '13': 'Surname',
            '14': 'Phone',
            '15': 'Birthday',

        },
    };
    $('.dropdown-item').click(function () {
        localStorage.setItem('dil', JSON.stringify($(this).attr('id')));
        location.reload();
    });

    var lang = JSON.parse(localStorage.getItem('dil'));

    if (lang == "tr") {
        $("#drop_yaz�").html("T�rk�e");
    }
    else {
        $("#drop_yaz�").html("English");

    }

    $('a,h5,p,h1,h2,span,li,button,h3,h4,label').each(function (index, element) {
        $(this).text(arrLang[lang][$(this).attr('key')]);

    });

});
