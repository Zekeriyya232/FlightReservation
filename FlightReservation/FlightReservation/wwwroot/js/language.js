$(document).ready(function () {
    var arrLang = {
        'tr': {
            '1': 'Kalkýþ Havalimaný Seçiniz',
            '2': 'Varýþ Havalimaný Seçiniz',
            '3': 'Kalkýþ Tarihi',
            '4': 'Uçuþ Ara',
            '5': 'Kalkýþ Tarihi',
            '6': 'Kalkýþ',
            '7': 'Varýþ',
            '8': 'Bilet Fiyat',
            '9': 'Bilet Ýþlemini Tamamla',
            '10': 'Kalkýþ Tarihi',
            '11': 'Düzenle',
            '12': 'Kullanýcý Adý',
            '13': 'Kullanýcý Soyadý',
            '14': 'Telefon',
            '15': 'Doðum Tarihi',
            
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
        $("#drop_yazý").html("Türkçe");
    }
    else {
        $("#drop_yazý").html("English");

    }

    $('a,h5,p,h1,h2,span,li,button,h3,h4,label').each(function (index, element) {
        $(this).text(arrLang[lang][$(this).attr('key')]);

    });

});
