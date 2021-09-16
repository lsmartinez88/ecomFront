
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
                label: {
                    normal: {
                        show: true,
                        formatter: function (params, ticket, callback) {
                            return  echarts.format.formatTime('dd', params.data[0]) ;
                        },
                        textStyle: {
                            fontSize: 10,
                            fontFamily: 'Roboto, sans-serif'
                        }
                    }
                },
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

var Prueba = [
    [
        {
            name: 'Navidad',
            xAxis: '25/12/2020'
        },
        {
            xAxis: '05/03/2021'
        }
    ],
    [
        {
            name: 'prueba 2',
            xAxis: '27/10/2020'
        },
        {
            xAxis: '27/10/2020'
        }
    ],
    [
        {
            //name: 'prueba 2',
            xAxis: '27/01/2021'
        },
        {
            xAxis: '28/01/2021'
        }
    ]
];


var _purchaseSoldQttyAndEvents = function (divName, Dates, Data, Events) {
    // Line chart with zoom
    if (typeof echarts == 'undefined') {
        console.warn('Warning - echarts.min.js is not loaded.');
        return;
    }

    // Define element
    var line_zoom_element = document.getElementById(divName);
    var colorLinea1 = colores[Math.floor(Math.random() * 7)];

    //
    // Charts configuration
    //

    if (line_zoom_element) {

        // Initialize chart
        var line_zoom = echarts.init(line_zoom_element);


        //
        // Chart config
        //

        // Options
        line_zoom.setOption({

            // Global text styles
            textStyle: {
                fontFamily: 'Roboto, Arial, Verdana, sans-serif',
                fontSize: 13
            },

            // Chart animation duration
            animationDuration: 750,

            // Setup grid
            grid: {
                left: 0,
                right: 40,
                top: 35,
                bottom: 60,
                containLabel: true
            },

            // Add legend
            //legend: {
            //    data: ['Cantidad-De-Ventas'],
            //    itemHeight: 8,
            //    itemGap: 20
            //},

            // Add tooltip
            tooltip: {
                trigger: 'axis',
                backgroundColor: 'rgba(0,0,0,0.75)',
                padding: [10, 15],
                textStyle: {
                    fontSize: 13,
                    fontFamily: 'Roboto, sans-serif'
                }
            },

            // Horizontal axis
            xAxis: [{
                type: 'category',
                boundaryGap: false,
                axisLabel: {
                    color: '#333'
                },
                axisLine: {
                    lineStyle: {
                        color: '#999'
                    }
                },
                data: Dates
            }],

            // Vertical axis
            yAxis: [{
                type: 'value',
                axisLabel: {
                    formatter: '{value} ',
                    color: '#333'
                },
                axisLine: {
                    lineStyle: {
                        color: '#999'
                    }
                },
                splitLine: {
                    lineStyle: {
                        color: ['#eee']
                    }
                },
                splitArea: {
                    show: true,
                    areaStyle: {
                        color: ['rgba(250,250,250,0.1)', 'rgba(0,0,0,0.01)']
                    }
                }
            }],

            // Zoom control
            dataZoom: [
                {
                    type: 'inside',
                    start: 0,
                    end: 100
                },
                {
                    show: true,
                    type: 'slider',
                    start: 30,
                    end: 70,
                    height: 50,
                    bottom: 0,
                    borderColor: '#fffff',
                    fillerColor: 'rgba(0,0,0,0.05)',
                    handleStyle: {
                        color: '#585f63'
                    }
                }
            ],

            // Add series
            series: [
                {
                    name: 'Cantidad-De-Ventas',
                    type: 'bar',
                    smooth: true,
                    lineStyle: {
                        width: 2
                    },
                    color: colorLinea1,
                    symbolSize: 2,
                    itemStyle: {
                        normal: {
                            borderWidth: 1
                        }
                    },
                    data: Data,
                    //markLine: {
                    //    symbol: ['none', 'none'],
                    //    itemStyle: {
                    //        color: 'rgba(255, 173, 177, 0.4)'
                    //    },
                    //    data: [
                    //        { name: 'asas', xAxis: '24/08/2020' },
                    //        { xAxis: '02/11/2020', name: 'asas' }
                    //    ]
                    //},
                    markArea: {
                        itemStyle: {
                            borderWidth: 3,
                            borderColor: 'rgba(69, 116, 138, 0.4)',
                            color: 'rgba(69, 116, 138, 0.4)'
                        },
                        data: Events,
                        label: {
                            rotate: 90,
                            lineOverflow: 'truncate',
                            elipsis: '...',
                            offset: [-5,0],
                            align:'right',
                            color: '#45748a'
                        },
                    }
                }
            ]
        });
    }


    //
    // Resize charts
    //

    // Resize function
    var triggerChartResize = function () {
        line_zoom_element && line_zoom.resize();
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


    //
    // Return objects assigned to module
    //

    return {
        init: function () {
            _linesZoomLightExample();
        }
    }
};