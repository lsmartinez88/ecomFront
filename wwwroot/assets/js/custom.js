/* ------------------------------------------------------------------------------
 *
 *  # Custom JS code
 *
 *  Place here all your custom js. Make sure it's loaded after app.js
 *
 * ---------------------------------------------------------------------------- */
var colores = ["#25b372", "#45748a", "#f58646", "#2196F3",    "#8e70c1", "#ffd648", "#f35c86", "#333",
    "#3F51B5", "#2196F3", "#00BCD4", "#e9f5fe",    "#45748a", "#e9f5fe", "#45748a", "#fdeeee",
    "#fde7da", "#25b372", "#d3f0e3", "#00BCD4",    "#ccf2f6", "#999", "#555", "#f58646",
    "#fdeeee", "#fff", "#eee", "#009688", "#cceae7",    "#d9dcf0", "#e8e2f3", "#fddee7", "#fff7da",
    "#dae3e8", "#d3f0e3", "#ccf2f6", "#fde7da"]

// Donut with details
var _donutWithDetails = function (element, size, data) {
    if (typeof d3 == 'undefined') {
        console.warn('Warning - El grafico no esta cargado');
        return;
    }

    // Initialize chart only if element exsists in the DOM
    if (element) {


        // Basic setup
        // ------------------------------

        // Add data set


        // Main variables
        var d3Container = d3.select(element),
            distance = 2, // reserve 2px space for mouseover arc moving
            radius = (size / 2) - distance,
            sum = d3.sum(data, function (d) { return d.value; });


        // Tooltip
        // ------------------------------

        var tip = d3.tip()
            .attr('class', 'd3-tip')
            .offset([-10, 0])
            .direction('e')
            .html(function (d) {
                return "<ul class='list-unstyled mb-1'>" +
                    "<li>" + "<div class='font-size-base my-1'>" + d.data.status + "</div>" + "</li>" +
                    "<li>" + "Total: &nbsp;" + "<span class='font-weight-semibold float-right'>" + d.value + "</span>" + "</li>" +
                    "<li>" + "Share: &nbsp;" + "<span class='font-weight-semibold float-right'>" + (100 / (sum / d.value)).toFixed(2) + "%" + "</span>" + "</li>" +
                    "</ul>";
            });


        // Create chart
        // ------------------------------

        // Add svg element
        var container = d3Container.append("svg").call(tip);

        // Add SVG group
        var svg = container
            .attr("width", size)
            .attr("height", size)
            .append("g")
            .attr("transform", "translate(" + (size / 2) + "," + (size / 2) + ")");


        // Construct chart layout
        // ------------------------------

        // Pie
        var pie = d3.layout.pie()
            .sort(null)
            .startAngle(Math.PI)
            .endAngle(3 * Math.PI)
            .value(function (d) {
                return d.value;
            });

        // Arc
        var arc = d3.svg.arc()
            .outerRadius(radius)
            .innerRadius(radius / 1.35);


        //
        // Append chart elements
        //

        // Group chart elements
        var arcGroup = svg.selectAll(".d3-arc")
            .data(pie(data))
            .enter()
            .append("g")
            .attr("class", "d3-arc d3-slice-border")
            .style({
                'cursor': 'pointer'
            });

        // Append path
        var arcPath = arcGroup
            .append("path")
            .style("fill", function (d) {
                return d.data.color;
            });


        //
        // Add interactions
        //

        // Mouse
        arcPath
            .on('mouseover', function (d, i) {

                // Transition on mouseover
                d3.select(this)
                    .transition()
                    .duration(500)
                    .ease('elastic')
                    .attr('transform', function (d) {
                        d.midAngle = ((d.endAngle - d.startAngle) / 2) + d.startAngle;
                        var x = Math.sin(d.midAngle) * distance;
                        var y = -Math.cos(d.midAngle) * distance;
                        return 'translate(' + x + ',' + y + ')';
                    });

                $(element + ' [data-slice]').css({
                    'opacity': 0.3,
                    'transition': 'all ease-in-out 0.15s'
                });
                $(element + ' [data-slice=' + i + ']').css({ 'opacity': 1 });
            })
            .on('mouseout', function (d, i) {

                // Mouseout transition
                d3.select(this)
                    .transition()
                    .duration(500)
                    .ease('bounce')
                    .attr('transform', 'translate(0,0)');

                $(element + ' [data-slice]').css('opacity', 1);
            });

        // Animate chart on load
        arcPath
            .transition()
            .delay(function (d, i) {
                return i * 500;
            })
            .duration(500)
            .attrTween("d", function (d) {
                var interpolate = d3.interpolate(d.startAngle, d.endAngle);
                return function (t) {
                    d.endAngle = interpolate(t);
                    return arc(d);
                };
            });


        //
        // Add text
        //

        // Total
        svg.append('text')
            .attr({
                'class': 'half-donut-total d3-text opacity-50',
                'text-anchor': 'middle',
                'dy': -13
            })
            .text('Total');

        // Count
        svg
            .append('text')
            .attr('class', 'half-donut-count d3-text')
            .attr('text-anchor', 'middle')
            .attr('dy', 14)
            .style({
                'font-size': '21px',
                'font-weight': 500
            });

        // Animate count
        svg.select('.half-donut-count')
            .transition()
            .duration(1500)
            .ease('linear')
            .tween("text", function (d) {
                var i = d3.interpolate(this.textContent, sum);

                return function (t) {
                    this.textContent = d3.format(",d")(Math.round(i(t)));
                };
            });


        //
        // Add legend
        //

        // Append list
        var legend = d3.select(element)
            .append('ul')
            .attr('class', 'chart-widget-legend')
            .selectAll('li')
            .data(pie(data))
            .enter()
            .append('li')
            .attr('data-slice', function (d, i) {
                return i;
            })
            .attr('style', function (d, i) {
                return 'border-bottom: solid 3px ' + d.data.color;
            })
            .text(function (d, i) {
                return d.data.status + ': ';
            });

        // Append text
        legend.append('span')
            .text(function (d, i) {
                return d.data.value;
            });
    }
};

// Donut with legend
var _animatedDonutWithLegend = function (element, size, data) {
    if (typeof d3 == 'undefined') {
        console.warn('Warning - d3.min.js is not loaded.');
        return;
    }

    // Initialize chart only if element exsists in the DOM
    if (element) {

        // Main variables
        var d3Container = d3.select(element),
            distance = 2, // reserve 2px space for mouseover arc moving
            radius = (size / 2) - distance,
            sum = d3.sum(data, function (d) { return d.value; });


        // Create chart
        // ------------------------------

        // Add svg element
        var container = d3Container.append("svg");

        // Add SVG group
        var svg = container
            .attr("width", size)
            .attr("height", size)
            .append("g")
            .attr("transform", "translate(" + (size / 2) + "," + (size / 2) + ")");


        // Construct chart layout
        // ------------------------------

        // Pie
        var pie = d3.layout.pie()
            .sort(null)
            .startAngle(Math.PI)
            .endAngle(3 * Math.PI)
            .value(function (d) {
                return d.value;
            });

        // Arc
        var arc = d3.svg.arc()
            .outerRadius(radius)
            .innerRadius(radius / 1.5);


        //
        // Append chart elements
        //

        // Group chart elements
        var arcGroup = svg.selectAll(".d3-arc")
            .data(pie(data))
            .enter()
            .append("g")
            .attr("class", "d3-arc d3-slice-border")
            .style({
                'cursor': 'pointer'
            });

        // Append path
        var arcPath = arcGroup
            .append("path")
            .style("fill", function (d) {
                return d.data.color;
            });


        // Add interactions
        arcPath
            .on('mouseover', function (d, i) {

                // Transition on mouseover
                d3.select(this)
                    .transition()
                    .duration(500)
                    .ease('elastic')
                    .attr('transform', function (d) {
                        d.midAngle = ((d.endAngle - d.startAngle) / 2) + d.startAngle;
                        var x = Math.sin(d.midAngle) * distance;
                        var y = -Math.cos(d.midAngle) * distance;
                        return 'translate(' + x + ',' + y + ')';
                    });

                // Animate legend
                $(element + ' [data-slice]').css({
                    'opacity': 0.3,
                    'transition': 'all ease-in-out 0.15s'
                });
                $(element + ' [data-slice=' + i + ']').css({ 'opacity': 1 });
            })
            .on('mouseout', function (d, i) {

                // Mouseout transition
                d3.select(this)
                    .transition()
                    .duration(500)
                    .ease('bounce')
                    .attr('transform', 'translate(0,0)');

                // Revert legend animation
                $(element + ' [data-slice]').css('opacity', 1);
            });

        // Animate chart on load
        arcPath
            .transition()
            .delay(function (d, i) {
                return i * 500;
            })
            .duration(500)
            .attrTween("d", function (d) {
                var interpolate = d3.interpolate(d.startAngle, d.endAngle);
                return function (t) {
                    d.endAngle = interpolate(t);
                    return arc(d);
                };
            });


        //
        // Append counter
        //

        // Append text
        svg
            .append('text')
            .attr('class', 'd3-text')
            .attr('text-anchor', 'middle')
            .attr('dy', 6)
            .style({
                'font-size': '17px',
                'font-weight': 500
            });

        // Animate text
        svg.select('text')
            .transition()
            .duration(1500)
            .tween("text", function (d) {
                var i = d3.interpolate(this.textContent, sum);
                return function (t) {
                    this.textContent = d3.format(",d")(Math.round(i(t)));
                };
            });


        //
        // Append legend
        //

        // Add element
        var legend = d3.select(element)
            .append('ul')
            .attr('class', 'chart-widget-legend')
            .selectAll('li').data(pie(data))
            .enter().append('li')
            .attr('data-slice', function (d, i) {
                return i;
            })
            .attr('style', function (d, i) {
                return 'border-bottom: 2px solid ' + d.data.color;
            })
            .text(function (d, i) {
                return d.data.status + ': ';
            });

        // Add value
        legend.append('span')
            .text(function (d, i) {
                return d.data.value;
            });
    }
};


// Progress arc - single color
var _progressArcSingle = function (element, size, colorContainter, min, max, valor) {
    if (typeof d3 == 'undefined') {
        console.warn('Warning - d3.min.js is not loaded.');
        return;
    }

    // Initialize chart only if element exsists in the DOM
    if (element) {

        // Main variables
        var d3Container = d3.select(element),
            radius = size,
            thickness = 20,
            color = colorContainter;


        // Create chart
        // ------------------------------

        // Add svg element
        var container = d3Container.append("svg");

        // Add SVG group
        var svg = container
            .attr('width', radius * 2)
            .attr('height', radius + 20)
            .attr('class', 'gauge');


        // Construct chart layout
        // ------------------------------

        // Pie
        var arc = d3.svg.arc()
            .innerRadius(radius - thickness)
            .outerRadius(radius)
            .startAngle(-Math.PI / 2);


        // Append chart elements
        // ------------------------------

        //
        // Group arc elements
        //

        // Group
        var chart = svg.append('g')
            .attr('transform', 'translate(' + radius + ',' + radius + ')');

        // Background
        var background = chart.append('path')
            .datum({
                endAngle: Math.PI / 2
            })
            .attr({
                'd': arc,
                'class': 'd3-state-empty'
            });

        // Foreground
        var foreground = chart.append('path')
            .datum({
                endAngle: -Math.PI / 2
            })
            .style('fill', color)
            .attr('d', arc);

        // Counter value
        var value = svg.append('g')
            .attr('transform', 'translate(' + radius + ',' + (radius * 0.9) + ')')
            .append('text')
            .text(0 + '%')
            .attr({
                'class': 'd3-text',
                'text-anchor': 'middle'
            })
            .style({
                'font-size': 19,
                'font-weight': 400
            });


        //
        // Min and max text
        //

        // Group
        var scale = svg.append('g')
            .attr('transform', 'translate(' + radius + ',' + (radius + 15) + ')')
            .attr('class', 'd3-text opacity-75')
            .style({
                'font-size': 12
            });

        // Max
        scale.append('text')
            .text(max)
            .attr({
                'text-anchor': 'middle',
                'x': (radius - thickness / 2)
            });

        // Min
        scale.append('text')
            .text(min)
            .attr({
                'text-anchor': 'middle',
                'x': -(radius - thickness / 2)
            });


        //
        // Animation
        //

        update(valor);

        // Update
        function update(v) {
            v = d3.format('.0f')(v);
            foreground.transition()
                .duration(750)
                .call(arcTween, v);

            value.transition()
                .duration(750)
                .call(textTween, v);
        }

        // Arc
        function arcTween(transition, v) {
            var newAngle = v / 100 * Math.PI - Math.PI / 2;
            transition.attrTween('d', function (d) {
                var interpolate = d3.interpolate(d.endAngle, newAngle);
                return function (t) {
                    d.endAngle = interpolate(t);
                    return arc(d);
                };
            });
        }

        // Text
        function textTween(transition, v) {
            transition.tween('text', function () {
                var interpolate = d3.interpolate(this.innerHTML, v),
                    split = (v + '').split('.'),
                    round = (split.length > 1) ? Math.pow(10, split[1].length) : 1;
                return function (t) {
                    this.innerHTML = d3.format('.0f')(Math.round(interpolate(t) * round) / round) + '<tspan>%</tspan>';
                };
            });
        }
    }
};



    // Scatter punch chart
    var _scatterPlotGroupingPrice = function (DivName, xList,xName, yList, yName, data, dataName,minValue, maxValue, data2) {
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

            data2 = data2.map(function (item) {
                return [item[1], item[0], item[2]];
            });

            // Options
            scatter_punch.setOption({

                // Add legend
                legend: {
                    data: ['Medio-De-Pago', 'Forma-Envio'],
                    align:'right'
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
                    boundaryGap:true,
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
                        interval:0,
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
                    name: 'Medio-De-Pago',
                    type: 'scatter',
                    symbolSize: function (val) {
                        if (val[2] == 0)
                            return 0;
                        else 
                            return (((60)/(maxValue)) * val[2] ) + 3;
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
                },
                    {
                        name: 'Forma-Envio',
                        type: 'scatter',
                        symbolSize: function (val) {
                            if (val[2] == 0)
                                return 0;
                            else
                                return (((val[2]) / (maxValue)) * 50) + 3;
                        },
                        data: data2,
                        itemStyle: {
                            color: colores[Math.floor(Math.random() * 6)],
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
