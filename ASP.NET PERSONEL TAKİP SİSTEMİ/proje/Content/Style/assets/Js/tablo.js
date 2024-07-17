function openModal(ad, soyad, tc, adres, cinsiyet, telefon, pozisyon, maas) {
    var modal = document.getElementById("myModal");
    var form = document.getElementById("editForm");

    // Clear previous form data
    form.innerHTML = "";

    // Populate form fields with employee data
    form.innerHTML += "<label for='ad'>Ad:</label><input type='text' name='ad'  id='ad' value='" + ad + "'><br>";
    form.innerHTML += "<label for='soyad'>Soyad:</label><input type='text' name='soyad' id='soyad' value='" + soyad + "'><br>";
    form.innerHTML += "<label for='tc'>TC:</label><input type='text' id='tc' name='tc' value='" + tc + "'><br>";
    form.innerHTML += "<label for='adres'>Adres:</label><input type='text' name='adres' id='adres' value='" + adres + "'><br>";
    form.innerHTML += "<label for='cinsiyet'>Cinsiyet:</label><input type='text' name='cinsiyet' id='cinsiyet' value='" + cinsiyet + "'><br>";
    form.innerHTML += "<label for='telefon'>Telefon:</label><input type='text' name='telefon' id='telefon' value='" + telefon + "'><br>";
    form.innerHTML += "<label for='pozisyon'>Pozisyon:</label><input type='text' name='pozisyon' id='pozisyon' value='" + pozisyon + "'><br>";
    form.innerHTML += "<label for='maas'>Maaş:</label><input type='text'name='maas'  id='maas' value='" + maas + "'><br>";
    form.innerHTML += "<input type='submit' value='Kaydet'>";

    modal.style.display = "block";
}

// Kapatma işlemi
var span = document.getElementsByClassName("close")[0];
span.onclick = function () {
    modal.style.display = "none";
}

// Kullanıcı modal dışına tıkladığında kapatma işlemi
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}



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