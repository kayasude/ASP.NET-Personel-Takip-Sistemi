
document.getElementById("searchInput").addEventListener("keyup", function () {
    var input, filter, table, tr, td, i, j, txtValue;
    input = document.getElementById("searchInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td");
        for (j = 0; j < td.length; j++) {
            if (td[j]) {
                txtValue = td[j].textContent || td[j].innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                    break; // Eğer bir eşleşme bulunursa, döngüyü sonlandırın
                } else {
                    tr[i].style.display = "none";
                }
            }
        }
    }
});





function confirmDelete(personelID) {
    if (confirm("Bu kaydı silmek istediğinize emin misiniz?")) {
        // Silme işlemini gerçekleştirmek için AJAX isteği gönder
        $.ajax({
            url: '/Personel/personelsil/' + personelID,
            type: 'POST',
            success: function (result) {
                // Silme işlemi başarılı olduğunda yapılacak işlemler
                alert("Silme işlemi başarıyla gerçekleştirildi.");
                // Belirli bir sayfaya yönlendirme
                window.location.href = '/Firma/firmabilgisi'; // Yönlendirilecek sayfanın URL'si
            },

        }
        );
    }
}
$(document).ready(function () {
    // Pozisyon ve cinsiyet menülerini gizle
    $('.dropdown-menu').hide();

    // Pozisyon düğmesine tıklandığında pozisyon menüsünü aç
    $('#positionDropdown').click(function () {
        $('#positionDropdown + .dropdown-menu').toggle();
        $('#genderDropdown + .dropdown-menu').hide();
        $('#subeDropdown + .dropdown-menu').hide();


    });

    // Cinsiyet düğmesine tıklandığında cinsiyet menüsünü aç
    $('#genderDropdown').click(function () {
        $('#genderDropdown + .dropdown-menu').toggle();
        $('#positionDropdown + .dropdown-menu').hide();
        $('#subeDropdown + .dropdown-menu').hide();

    });

    $('#subeDropdown').click(function () {
        $('#subeDropdown + .dropdown-menu').toggle();
        $('#genderDropdown + .dropdown-menu').hide();
        $('#positionDropdown + .dropdown-menu').hide();
    });

    // Tüm personelleri göstermek için "Tümü" butonunu etkinleştir
    $('.filterButton[data-filter="all"]').addClass('active');

    // Filtreleme butonlarına tıklandığında
    $('.filterButton').click(function () {
        // Butonları temizle
        $('.filterButton').removeClass('active');
        // Tıklanan butonu etkinleştir
        $(this).addClass('active');

        var filterValue = $(this).data('filter');

        // Tüm satırları göster
        if (filterValue === 'all') {
            $('#myTable tbody tr').show();
        } else {
            // Filtrelenmemiş satırları gizle
            $('#myTable tbody tr').hide();
            // Filtreye uygun olanları göster
            $('#myTable tbody tr[data-position="' + filterValue + '"]').show();
            $('#myTable tbody tr[data-cinsiyet="' + filterValue + '"]').show();
            $('#myTable tbody tr[data-adres="' + filterValue + '"]').show();

        }
    });
});