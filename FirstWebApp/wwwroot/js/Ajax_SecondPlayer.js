var timeReload = 1;//время в сек
var timenow = 0;

function Recheck() {
	console.log("Checked!");
	$.ajax(
		{
			type: 'POST',
			url: '../Api/IsSecondPlayerInGame',
			success: function (result) {
				console.log('Data received: ');
				console.log(result);

				if (result.isSecondPlayer || result.isSessionExpiration) {
					location.reload();
				}
			}
		}
	)
}
var t = setInterval(Recheck, 1000);