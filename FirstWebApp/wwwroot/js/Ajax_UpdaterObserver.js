function Recheck() {
	$.ajax(
		{
			type: 'POST',
			url: '../Api/GetUsers',
			success: function (result) {
				console.log("Reloading...");
					location.reload();
			}
		}
	)
}
var t = setInterval(Recheck, 250);
