
// Single calendar 
var _calendarPurchaseOrdersQtty = function (divName, salesData, minValue, maxValue, minDate, maxDate) {
    if (typeof echarts == 'undefined') {
        console.warn('Warning - echarts.min.js is not loaded.');
        return;
    }

    // Define element
    var calendar_single_element = document.getElementById(divName);


    //
    // Charts configuration
    //

    if (calendar_single_element) {

        // Initialize chart
        var calendar_single = echarts.init(calendar_single_element);
        //
        // Chart config
        //

        // Demo data
        //function getVirtulData(year) {
        //    year = year || '2017';
        //    var date = +echarts.number.parseDate(year + '-01-01');
        //    var end = +echarts.number.parseDate((+year + 1) + '-01-01');
        //    var dayTime = 3600 * 24 * 1000;
        //    var data = [];
        //    for (var time = date; time < end; time += dayTime) {
        //        data.push([
        //            echarts.format.formatTime('yyyy-MM-dd', time),
        //            Math.floor(Math.random() * 10000)
        //        ]);
        //    }
        //    return data;
        //}

        // Options
        calendar_single.setOption({

            // Add tooltip
            tooltip: {
                trigger: 'item',
                backgroundColor: 'rgba(0,0,0,0.75)',
                padding: [10, 15],
                //formatter: "Fecha: " + '{c}'.split(",")[0] + " <br> Cantidad : " + '{c}'.split(",")[1],
                formatter: function(params, ticket, callback) {
                    debugger;
                    console.log(params.name);
                    return "Fecha: " + echarts.format.formatTime('dd-MM-yyyy', params.data[0]) + "<br> " + params.marker + params.data[1];
                },
                textStyle: {
                    fontSize: 13,
                    fontFamily: 'Roboto, sans-serif'
                }
            },

                        // Add legend
            legend: {
                orient: 'vertical',
                top: 'center',
                left: 0,
                data: ['IE', 'Opera', 'Safari', 'Firefox', 'Chrome'],
                itemHeight: 8,
                itemWidth: 8
            },

            // Visual map
            visualMap: {
                min: minValue,
                max: maxValue,
                calculable: true, 
                orient: 'horizontal',
                left: 'center',
                text: ['Bajo', 'Alto'],
                textGap: 20,
                itemHeight: 500,
                bottom: 0,
                textStyle: {
                    fontSize: 12
                }
            },

            // Calendar
            calendar: {
                top: 20,
                left: 70,
                right: 5,
                bottom:70,
                cellSize: ['auto', 22],
                range: [minDate, maxDate],
                itemStyle: {
                    normal: {
                        borderWidth: 1,
                        borderColor: '#fff'
                    }
                },
                splitLine: {
                    lineStyle: {
                        color: '#333',
                        width: 1
                    }
                },
                yearLabel: {
                    margin: 50,
                    fontWeight: 500
                },
                dayLabel: {
                    firstDay: 1,
                    nameMap: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa']
                },
                monthLabel: {
                    nameMap: [
                        'Ene', 'Feb', 'Mar',
                        'Abr', 'May', 'Jun',
                        'Jul', 'Ago', 'Sep',
                        'Oct', 'Nov', 'Dic'
                    ],
                }
            },

            // Add series
            series: [{
                type: 'heatmap',
                coordinateSystem: 'calendar',
                data: salesData,
                emphasis: {
                    itemStyle: {
                        shadowBlur: 10,
                        shadowColor: 'rgba(0, 0, 0, 0.7)'
                    }
                }
            }]
        });
    }


    //
    // Resize charts
    //

    // Resize function
    var triggerChartResize = function () {
        calendar_single_element && calendar_single.resize();
    };

    // On sidebar width change
    var sidebarToggle = document.querySelectorAll('.sidebar-control');
    if (sidebarToggle) {
        sidebarToggle.forEach(function (togglers) {
            togglers.addEventListener('click', triggerChartResize);
        });
    }

    // On window resize
    var resizeCharts;
    window.addEventListener('resize', function () {
        clearTimeout(resizeCharts);
        resizeCharts = setTimeout(function () {
            triggerChartResize();
        }, 200);
    });
};
