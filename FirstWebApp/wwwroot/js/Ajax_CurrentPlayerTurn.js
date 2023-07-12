var timeReload = 1;//время в сек
var timenow = 0;

function Recheck() {
	console.log("Checking");
	$.ajax(
		{
			type: 'POST',
			url: '../Api/IsCurrentPlayerTurn',
			success: function (result) {
				console.log('Data received: ');
				console.log(result);

				if (result.isCurrentPlayerTurn|| result.isSessionExpiration) {
					location.reload();
				}
			}
		}
	)
}
var t = setInterval(Recheck, 1000);
