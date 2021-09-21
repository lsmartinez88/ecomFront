var dataTable;
function initCloud(data, tipo) {
    if (tipo == 1) {
        $('#wordCloud').jQCloud(data, {
            classPattern: 'w{n}',
            colors: ['#2196F3', '#f35c86', '#25b372', '#f58646', '#3F51B5', '#009688', '#ef5350', '#45748a', '#8e70c1'],
            autoResize: true,
            steps: 9,
            fontSize: {
                //from: 0.15,
                from: 0.15,
                to: 0.025
            },
            delayedMode: true
        });
    } else if (tipo == 2) {
        $('#wordCloud2').jQCloud(data, {
            classPattern: 'w{n}',
            colors: ['#2196F3', '#f35c86', '#25b372', '#f58646', '#3F51B5', '#009688', '#ef5350', '#45748a', '#8e70c1'],
            autoResize: true,
            steps: 9,
            fontSize: {
                from: 0.15,
                to: 0.025
            },
            delayedMode: true
        });
    } else if (tipo == 3) {
        $('#wordCloud3').jQCloud(data, {
            classPattern: 'w{n}',
            colors: ['#2196F3', '#f35c86', '#25b372', '#f58646', '#3F51B5', '#009688', '#ef5350', '#45748a', '#8e70c1'],
            autoResize: true,
            steps: 9,
            fontSize: {
                from: 0.062,
                to: 0.015
            },
            delayedMode: true
        });
    }
}

function initTreemap(datos) {

    dataTable = google.visualization.arrayToDataTable(datos);
    tree = new google.visualization.TreeMap(document.getElementById('trendTreemap'));

    tree.draw(dataTable, {
            minColor: '#32CD32',
            midColor: '#ff0',
            maxColor: '#f00',
            headerHeight: 15,
            fontColor: 'black',
            showScale: true,
            generateTooltip: showFullTooltip
        });
}

function showFullTooltip(row, size, value) {
    if (dataTable.getValue(row, 4) === null)
        return '';
    else
        return '<div style="background:#fff; padding:10px; border-style:solid">' +
        '<span style="font-family:Courier"><b>Cantidad de Ventas: ' + dataTable.getValue(row, 4) +
        '</b>,Cantidad de Publicaciones: ' + dataTable.getValue(row, 5) + ', Posicion Tendencia: ' + dataTable.getValue(row, 6) +
        ', Ventas Por Publicacion: ' + dataTable.getValue(row, 7) + '</span></div>';
}

//colors: ['#2196F3', '#f35c86', '#25b372', '#f58646', '#ef5350', '#009688', '#3F51B5', '#45748a', '#8e70c1'],




// Single calendar 
var _barChartOportunity = function (divName, salesData, minValue, maxValue, minDate, maxDate) {
    if (typeof echarts == 'undefined') {
        console.warn('Warning - echarts.min.js is not loaded.');
        return;
    }

    var chartDom = document.getElementById('barChartOportunity');
    var myChart = echarts.init(chartDom);
    var option;

    myChart.setOption({
        dataset: {
            source: [
                ['score', 'amount', 'product'],
                [89.3, 58212, 'Matcha Latte'],
                [57.1, 78254, 'Milk Tea'],
                [74.4, 41032, 'Cheese Cocoa'],
                [50.1, 12755, 'Cheese Brownie'],
                [89.7, 20145, 'Matcha Cocoa'],
                [68.1, 79146, 'Tea'],
                [19.6, 91852, 'Orange Juice'],
                [10.6, 101852, 'Lemon Juice'],
                [32.7, 20112, 'Walnut Brownie']
            ]
        },
        grid: { containLabel: true },
        xAxis: { name: 'Cantidad apariciones en Tendencias' },
        yAxis: { type: 'Palabra' },
        visualMap: {
            orient: 'horizontal',
            left: 'center',
            min: 10,
            max: 100,
            text: ['Oportunidad Alta', 'Oportunidad Media'],
            // Map the score column to color
            dimension: 0,
            inRange: {
                color: ['#65B581', '#FFCE34', '#FD665F']
            }
        },
        series: [
            {
                type: 'bar',
                encode: {
                    // Map the "amount" column to X axis.
                    x: 'amount',
                    // Map the "product" column to Y axis
                    y: 'product'
                }
            }
        ]
    });

};
