$(document).ready(function () {
    if (typeof refreshInterval !== 'undefined' && typeof matchId !== 'undefined') {
        setInterval(refreshMatchMinutes, refreshInterval * 1000); // Assuming SettingSecondsInOneMinute is in seconds and setInterval expects milliseconds

        function refreshMatchMinutes() {
            $.ajax({
                url: '/Admin/Match/GetMatchMinute',
                data: { matchId: matchId },
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    $('#match-minutes').html(`<p>${data.minutes} min</p>`);
                },
                error: function () {
                    console.log('Error retrieving match minutes.');
                }
            });
        }
    }
});