var timeReload = 1;//время в сек
var timenow = 0;

function Recheck(tableGuid, Id) {
	console.log("Checking");
	$.ajax(
		{
			type: 'POST',
			url: '../Api/GetTableHTML/' + tableGuid,
			success: function (result) {
				console.log('Data received: ');
				console.log(result);

				$(Id).html(result);
			}
		}
	)
}

function SetUpdater(tableGuid, Id)
{
	var t = setInterval(Recheck, 1000, tableGuid, Id);
}

