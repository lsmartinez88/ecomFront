var _scatterPlotGroupingPrice = function (DivName, xList, xName, yList, yName, data, dataName, minValue, maxValue, legend) {
    if (typeof echarts == 'undefined') {
        console.warn('Warning - echarts.min.js is not loaded.');
        return;
    }

    // Define element
    var scatter_punch_element = document.getElementById(DivName);


    //
    // Charts configuration
    //

    if (scatter_punch_element) {

        // Initialize chart
        var scatter_punch = echarts.init(scatter_punch_element);

        //
        // Chart config
        //
        /*
        // Demo data
        var hours = ['12am', '1am', '2am', '3am', '4am', '5am', '6am', '7am', '8am', '9am', '10am', '11am', '12pm', '1pm', '2pm', '3pm', '4pm', '5pm', '6pm', '7pm', '8pm', '9pm', '10pm', '11pm'];
        var days = ['Saturday', 'Friday', 'Thursday', 'Wednesday', 'Tuesday', 'Monday', 'Sunday'];
        var data = [
            [0, 0, 15], [0, 1, 11], [0, 2, 6], [0, 3, 16], [0, 4, 14], [0, 5, 10], [0, 6, 4], [0, 7, 12], [0, 8, 4], [0, 9, 20], [0, 10, 17], [0, 11, 2], [0, 12, 4], [0, 13, 1], [0, 14, 1], [0, 15, 3], [0, 16, 4], [0, 17, 6], [0, 18, 4], [0, 19, 4], [0, 20, 3], [0, 21, 3], [0, 22, 2], [0, 23, 5],
            [1, 0, 7], [1, 1, 10], [1, 2, 9], [1, 3, 17], [1, 4, 8], [1, 5, 4], [1, 6, 7], [1, 7, 2], [1, 8, 0], [1, 9, 0], [1, 10, 5], [1, 11, 2], [1, 12, 2], [1, 13, 6], [1, 14, 9], [1, 15, 11], [1, 16, 6], [1, 17, 7], [1, 18, 8], [1, 19, 12], [1, 20, 5], [1, 21, 5], [1, 22, 7], [1, 23, 2],
            [2, 0, 1], [2, 1, 1], [2, 2, 8], [2, 3, 10], [2, 4, 6], [2, 5, 4], [2, 6, 12], [2, 7, 20], [2, 8, 16], [2, 9, 10], [2, 10, 3], [2, 11, 2], [2, 12, 1], [2, 13, 9], [2, 14, 8], [2, 15, 10], [2, 16, 6], [2, 17, 5], [2, 18, 5], [2, 19, 5], [2, 20, 7], [2, 21, 4], [2, 22, 2], [2, 23, 4],
            [3, 0, 7], [3, 1, 3], [3, 2, 10], [3, 3, 0], [3, 4, 4], [3, 5, 0], [3, 6, 0], [3, 7, 4], [3, 8, 1], [3, 9, 0], [3, 10, 5], [3, 11, 4], [3, 12, 7], [3, 13, 14], [3, 14, 13], [3, 15, 12], [3, 16, 9], [3, 17, 5], [3, 18, 5], [3, 19, 10], [3, 20, 6], [3, 21, 4], [3, 22, 4], [3, 23, 1],
            [4, 0, 1], [4, 1, 3], [4, 2, 2], [4, 3, 6], [4, 4, 3], [4, 5, 1], [4, 6, 7], [4, 7, 16], [4, 8, 10], [4, 9, 2], [4, 10, 4], [4, 11, 4], [4, 12, 2], [4, 13, 4], [4, 14, 4], [4, 15, 14], [4, 16, 12], [4, 17, 1], [4, 18, 8], [4, 19, 5], [4, 20, 3], [4, 21, 7], [4, 22, 3], [4, 23, 0],
            [5, 0, 2], [5, 1, 1], [5, 2, 9], [5, 3, 3], [5, 4, 0], [5, 5, 0], [5, 6, 0], [5, 7, 0], [5, 8, 2], [5, 9, 0], [5, 10, 4], [5, 11, 1], [5, 12, 5], [5, 13, 10], [5, 14, 5], [5, 15, 7], [5, 16, 11], [5, 17, 6], [5, 18, 0], [5, 19, 5], [5, 20, 3], [5, 21, 4], [5, 22, 2], [5, 23, 0],
            [6, 0, 1], [6, 1, 0], [6, 2, 4], [6, 3, 0], [6, 4, 0], [6, 5, 6], [6, 6, 0], [6, 7, 15], [6, 8, 0], [6, 9, 0], [6, 10, 1], [6, 11, 0], [6, 12, 2], [6, 13, 1], [6, 14, 3], [6, 15, 4], [6, 16, 10], [6, 17, 0], [6, 18, 0], [6, 19, 0], [6, 20, 1], [6, 21, 2], [6, 22, 2], [6, 23, 6]
        ];
        */

        data = data.map(function (item) {
            return [item[1], item[0], item[2]];
        });


        // Options
        scatter_punch.setOption({

            // Add legend
            legend: {
                data: legend,
                align: 'right'
            },

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
                right: 10,
                top: 25,
                bottom: 0,
                containLabel: true,
                show: minValue
            },

            // Add tooltip
            tooltip: {
                trigger: 'item',
                backgroundColor: 'rgba(0,0,0,0.60)',
                padding: [10, 15],
                textStyle: {
                    fontSize: 13,
                    fontFamily: 'Roboto, sans-serif'
                },
                axisPointer: {
                    type: 'cross',
                    lineStyle: {
                        type: 'dashed',
                        width: 1
                    }
                },
                formatter: function (params) {
                    return '<div class="mb-1">' + yList[params.value[1]] + '</div>' + '<div class="mr-3">' + dataName + ': ' + params.value[2] + '</div>' + '<div class="mr-3">' + xName + ': ' + xList[params.value[0]] + '</div>';
                }
            },


            // Axis pointer
            axisPointer: [{
                label: {
                    backgroundColor: "#999",
                    shadowBlur: 0
                }
            }],

            // Horizontal axis
            xAxis: [{
                type: 'category',
                boundaryGap: true,
                data: xList,
                axisLabel: {
                    color: '#333'
                },
                axisLine: {
                    lineStyle: {
                        color: '#999'
                    }
                },
                axisLabel: {
                    interval: 0,
                    rotate: 45
                },
                splitLine: {
                    show: true,
                    interval: 0,
                    lineStyle: {
                        color: '#eee',
                        type: 'solid'
                    }
                }
            }],

            // Vertical axis
            yAxis: [{
                type: 'category',
                data: yList,
                axisLabel: {
                    color: '#333'
                },
                axisLine: {
                    lineStyle: {
                        color: '#999'
                    }
                },
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: ['#eee']
                    }
                },
                splitArea: {
                    show: true,
                    areaStyle: {
                        color: ['rgba(250,250,250,0.1)', 'rgba(0,0,0,0.015)']
                    }
                }
            }],


            // Add series
            series: [{
                name: legend,
                type: 'scatter',
                symbolSize: function (val) {
                    if (val[2] == 0)
                        return 0;
                    else
                        return (((60) / (maxValue)) * val[2]) + 3;
                },
                data: data,
                itemStyle: {
                    color: colores[Math.floor(Math.random() * 5)],
                    opacity: 0.7,
                    shadowBlur: 6,
                    shadowOffsetX: 0,
                    shadowOffsetY: 0,
                    shadowColor: 'rgba(0,0,0,0.3)'
                },
                animationDelay: function (idx) {
                    return idx * 10;
                }
            }]
        });
    }


    //
    // Resize charts
    //

    // Resize function
    var triggerChartResize = function () {
        scatter_punch_element && scatter_punch.resize();
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


/* ------------------------------------------------------------------------------
 *
 *  # Echarts - Line chart with zoom example
 *
 *  Demo JS code for line chart with zoom option [light theme]
 *
 * ---------------------------------------------------------------------------- */


// Setup module
// ------------------------------

var _purchasePricePerDayInfo = function (divName,Dates, Prices) {
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
            legend: {
                data: ['Precio-De-Venta'],
                itemHeight: 8,
                itemGap: 20
            },

            // Add tooltip
            tooltip: {
                trigger: 'axis',
                backgroundColor: 'rgba(0,0,0,0.75)',
                padding: [10, 15],
                textStyle: {
                    fontSize: 13,
                    fontFamily: 'Roboto, sans-serif'
                },
                formatter: function (params) {
                    return '<div class="mb-1">' + params[0].name + '</br>' + params[0].marker +" Precio de Venta:  $" +  params[0].value.toLocaleString("es-AR") + '</div>';
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
                    formatter: '$ {value} ',
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
                //{
                //    name: 'Software',
                //    type: 'line',
                //    smooth: true,
                //    symbolSize: 6,
                //    itemStyle: {
                //        normal: {
                //            borderWidth: 2
                //        }
                //    },
                //    data: [152.45, 156, 479, 442, 654, 835, 465, 704, 643, 136, 791, 254, 688, 119, 948, 316, 612, 378, 707, 404, 485, 226, 754, 142, 965, 364, 887, 395, 838, 113, 662]
                //},
                //{
                //    name: 'Hardware',
                //    type: 'line',
                //    smooth: true,
                //    symbolSize: 6,
                //    itemStyle: {
                //        normal: {
                //            borderWidth: 2
                //        }
                //    },
                //    data: [677, 907, 663, 137, 952, 408, 976, 772, 514, 102, 165, 343, 374, 744, 237, 662, 875, 462, 409, 259, 396, 744, 359, 618, 127, 596, 161, 574, 107, 914, 708]
                //},
                {
                    name: 'Precio-De-Venta',
                    type: 'line',
                    smooth: false,
                    lineStyle: {
                        width:1
                    },
                    color: colorLinea1,
                    symbolSize: 2,
                    itemStyle: {
                        normal: {
                            borderWidth: 1
                        }
                    },
                    data: Prices,
                    markLine: {
                        data: [{
                            type: 'average',
                            name: 'Average'
                        }]
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

var _SingleAxisScatterChart = function (element, dataSeriesPublicaciones, dataSeriesVentas, dataX, maximoPublicaciones, maximoVentas) {
    if (typeof echarts == 'undefined') {
        console.warn('Warning - echarts.min.js is not loaded.');
        return;
    }

    // Define element
    var basic_element = document.getElementById(element);


    //
    // Charts configuration
    //

    if (basic_element) {
        var scatter = echarts.init(basic_element);

        scatter.setOption({

            tooltip: {
                backgroundColor: 'rgba(0,0,0,0.60)',
                padding: [10, 15],
                textStyle: {
                    fontSize: 13,
                    fontFamily: 'Roboto, sans-serif'
                },
                formatter: function (params) {
                    return '<div class="mb-1">De ' + params.name + ': ' + params.value[1] + ' ' + params.seriesName + '</div>';
                }
            },
            title: [
                {
                    textBaseline: 'middle',
                    top: ((1.5) * 100) / 7 + '%',
                    text: "Publicaciones",
                    textStyle: {
                        fontFamily: 'Roboto, Arial, Verdana, sans-serif',
                        fontSize: 16
                    }
                },
                {
                    textBaseline: 'middle',
                    top: ((3.5) * 160) / 7 + '%',
                    text: "Ventas",
                    textStyle: {
                        fontFamily: 'Roboto, Arial, Verdana, sans-serif',
                        fontSize: 16
                    }
                }
            ],
            singleAxis: [
                {
                    left: 150,
                    type: 'category',
                    boundaryGap: false,
                    data: dataX,
                    top: (1 * 50) / 7 + 5 + '%',
                    height: 220 / 7 - 10 + '%',
                    axisLabel: {
                        interval: 1
                    }
                },
                {
                    left: 150,
                    type: 'category',
                    boundaryGap: false,
                    data: dataX,
                    top: (3 * 150) / 7 + 5 + '%',
                    height: 220 / 7 - 10 + '%',
                    axisLabel: {
                        interval: 1
                    }
                }
            ],
            series: [
                {
                    name: "Publicaciones",
                    singleAxisIndex: 0,
                    coordinateSystem: 'singleAxis',
                    type: 'scatter',
                    data: dataSeriesPublicaciones,
                    symbolSize: function (val) {
                        if (val[1] == 0)
                            return 0;
                        else
                            return (((45) / (maximoPublicaciones)) * val[1]) + 3;
                    },
                    itemStyle: {
                        color: colores[Math.floor(Math.random() * 5)],
                        opacity: 0.7,
                        shadowBlur: 6,
                        shadowOffsetX: 0,
                        shadowOffsetY: 0,
                        shadowColor: 'rgba(0,0,0,0.3)'
                    },
                    animationDelay: function (idx) {
                        return idx * 10;
                    }
                },
                {
                    name: "Ventas",
                    singleAxisIndex: 1,
                    coordinateSystem: 'singleAxis',
                    type: 'scatter',
                    data: dataSeriesVentas,
                    symbolSize: function (val) {
                        if (val[1] == 0)
                            return 0;
                        else
                            return (((45) / (maximoVentas)) * val[1]) + 3;
                    },
                    itemStyle: {
                        color: colores[Math.floor(Math.random() * 5)],
                        opacity: 0.7,
                        shadowBlur: 6,
                        shadowOffsetX: 0,
                        shadowOffsetY: 0,
                        shadowColor: 'rgba(0,0,0,0.3)'
                    },
                    animationDelay: function (idx) {
                        return idx * 10;
                    }
                }]
        });
    }


    //
    // Resize charts
    //

    // Resize function
    var triggerChartResize = function () {
        basic_element && basic_element.resize();
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


