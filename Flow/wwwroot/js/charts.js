google.charts.load('current', { 'packages': ['gauge', 'corechart'] });
google.charts.setOnLoadCallback(drawCharts);




function drawCharts() {
    
    $.ajax({
        url: "/Flow/GetWorkCompletionData",
        dataType: "JSON",
        success: function (result) {
            
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'name');
            data.addColumn('number', 'count');

            var dataArray = [];
            $.each(result, function (i, obj) {
                dataArray.push([obj.name, parseInt(obj.count)]);
            });
            data.addRows(dataArray);

            var options = {
                title: 'Completed Units by Employee',
                titleTextStyle: {
                    color: '#FFF',
                    bold: true,
                    fontSize: 16
                },
                backgroundColor: 'transparent',
                height: '100%',
                width: '',
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
                colors: ['green']
            };
            var chart = new google.visualization.ColumnChart(document.getElementById('employee-div'));
            chart.draw(data, options);
        }
    });
    $.ajax({
        url: "/Flow/GetWorkstationDepartmentData",
        dataType: "JSON",
        success: function (result) {
            
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
        }
    });

    $.ajax({
        url: "/Flow/GetUtilizationData",
        dataType: "JSON",
        success: function (result) {
            var data = google.visualization.arrayToDataTable([
                ['Label', 'Value'],
                ['Utilization %', result*100],
            ]);

            var options = {
                redFrom: 0, redTo: 25,
                yellowFrom: 25, yellowTo: 50,
                minorTicks: 5,
                width: 400
            };

            var chart = new google.visualization.Gauge(document.getElementById('utilization-div'));

            chart.draw(data, options);
        }
    });

}

