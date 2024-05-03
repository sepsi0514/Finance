$(function () {
    $(".datefield").datepicker();
});

<script>
    $(document).ready(function () {
        $(".datepicker").datepicker({
            dateFormat: "dd-mm-yy",
            changemonth: true,
            changeyear: true
        });
   });
</script>