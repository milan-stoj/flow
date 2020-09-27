$.ajax({
    url: "/Flow/GetChartData",
    dataType: "JSON",
    success: function (result) {
        google.charts.load('current', {
            'packages': ['corechart', 'bar']
        });
        google.charts.setOnLoadCallback(function () {
            drawCharts(result);
        })
    }
});

function drawCharts(result) {

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'department');
    data.addColumn('number', 'count');

    var dataArray = [];
    $.each(result, function (i, obj) {
        dataArray.push([obj.department, parseInt(obj.count)]);
    });
    data.addRows(dataArray);

    var options = {
        title: 'Workstations by Department',
        titleTextStyle: {
            color: '#FFF',
            bold: true,
            fontSize: 16
        },
        backgroundColor: 'transparent',
        height: '100%',
        width: '100%',
        legend: { position: 'none' },
        hAxis: {
            textStyle: {
                color: '#FFF',
                fontSize: 12
            }
        },
        vAxis: {
            textStyle: {
                color: '#FFF',
                fontSize: 12
            }
        },
        is3D: true,
        colors: ['red']
    };
    var chart = new google.visualization.BarChart(document.getElementById('barchart-div'));
    chart.draw(data, options);
};
