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


var _pieDonutBasic = function (elemento, data) {
    if (typeof d3 == 'undefined') {
        console.warn('Warning - d3.min.js is not loaded.');
        return;
    }

    // Main variables
    var element = document.getElementById(elemento);


    // Initialize chart only if element exsists in the DOM
    if (element) {


        // Basic setup
        // ------------------------------

        // Add data set


        // Main variables
        var d3Container = d3.select(element),
            distance = 2, // reserve 2px space for mouseover arc moving
            radius = 80;

        data.forEach(function (d) {
            d.text = d.text.replace('CantidadCompetidores_', '');
            d.text = d.text.charAt(0).toUpperCase() + d.text.slice(1);
        });



        // Tooltip
        // ------------------------------

        var tip = d3.tip()
            .attr('class', 'd3-tip')
            .offset([-10, 0])
            .direction('e')
            .html(function (d) {
                return '<ul class="list-unstyled mb-1">' +
                    '<li>' + '<div class="font-size-base mb-1 mt-1">' + d.data.text + '</div>' + '</li>' +
                    '<li>' + 'Cantidad: &nbsp;' + '<span class="font-weight-semibold float-right">' + d.value + '</span>' + '</li>' +
                    '</ul>';
            });


        // Create chart
        // ------------------------------

        // Add svg element
        var container = d3Container.append('svg').call(tip);

        // Add SVG group
        var svg = container
            .attr('width', radius*2)
            .attr('height', radius * 2)
            .append('g')
            .attr('transform', 'translate(' + (radius) + ',' + (radius) + ')');



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
            .innerRadius(radius / 2);




        //
        // Append chart elements
        //

        // Group chart elements
        var arcGroup = svg.selectAll('.d3-arc')
            .data(pie(data))
            .enter()
            .append('g')
            .attr('class', 'd3-arc d3-slice-border')
            .style('cursor', 'pointer');

        // Append path
        var arcPath = arcGroup
            .append('path')
            .style('fill', function (d) { return d.data.color; });

        // Add tooltip
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
            })

            .on('mousemove', function (d) {

                // Show tooltip on mousemove
                tip.show(d)
                    .style('top', (d3.event.pageY - 40) + 'px')
                    .style('left', (d3.event.pageX + 30) + 'px');
            })

            .on('mouseout', function (d, i) {

                // Mouseout transition
                d3.select(this)
                    .transition()
                    .duration(500)
                    .ease('bounce')
                    .attr('transform', 'translate(0,0)');

                // Hide tooltip
                tip.hide(d);
            });

        // Animate chart on load
        arcPath
            .transition()
            .delay(function (d, i) { return i * 500; })
            .duration(500)
            .attrTween('d', function (d) {
                var interpolate = d3.interpolate(d.startAngle, d.endAngle);
                return function (t) {
                    d.endAngle = interpolate(t);
                    return arc(d);
                };
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


var _linesStacked = function (element, dataX, dataLegend,dataSeries) {
    if (typeof echarts == 'undefined') {
        console.warn('Warning - echarts.min.js is not loaded.');
        return;
    }

    // Define element
    var line_stacked_element = document.getElementById(element);


    //
    // Charts configuration
    //

    if (line_stacked_element) {

        // Initialize chart
        var line_stacked = echarts.init(line_stacked_element);


        //
        // Chart config
        //

        // Options
        line_stacked.setOption({

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
                right: 20,
                top: 35,
                bottom: 0,
                containLabel: true
            },

            // Add legend
            legend: {
                data: dataLegend,
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
                formatter: function (params, ticket, callback) {
                    return "Ejecucion: " + params[0].axisValue + "<br> " + params[0].marker + " $ " + parseFloat(params[0].data).toFixed(2) + "<br> " + params[1].marker + " $ " + parseFloat(params[1].data).toFixed(2);
                },
            },

            // Horizontal axis
            xAxis: [{
                type: 'category',
                boundaryGap: false,
                data: dataX,
                axisLabel: {
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
                }
            }],

            // Vertical axis
            yAxis: [{
                type: 'value',
                axisLabel: {
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

            // Add series
            series: dataSeries
        });
    }


    //
    // Resize charts
    //

    // Resize function
    var triggerChartResize = function () {
        line_stacked_element && line_stacked.resize();
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


var _funnelBasicLightExample = function (element,dataSeries, dataLegend, valuesInd) {
    if (typeof echarts == 'undefined') {
        console.warn('Warning - echarts.min.js is not loaded.');
        return;
    }

    // Define element
    var funnel_basic_element = document.getElementById(element);


    //
    // Charts configuration
    //

    if (funnel_basic_element) {

        // Initialize chart
        var funnel_basic = echarts.init(funnel_basic_element);


        //
        // Chart config
        //

        // Options
        funnel_basic.setOption({

            // Colors
            color:colores,

            // Global text styles
            textStyle: {
                fontFamily: 'Roboto, Arial, Verdana, sans-serif',
                fontSize: 13
            },

            // Add title
            title: {
                text: 'Como se llega a una venta',
                left: 'center',
                textStyle: {
                    fontSize: 18,
                    fontWeight: 400
                },
                subtextStyle: {
                    fontSize: 14
                }
            },

            // Add tooltip
            tooltip: {
                trigger: 'item',
                backgroundColor: 'rgba(0,0,0,0.75)',
                padding: [10, 15],
                textStyle: {
                    fontSize: 13,
                    fontFamily: 'Roboto, sans-serif'
                },
                formatter: function (params, ticket, callback) {
                    return params.marker + params.data.name + ": " + params.data.cantidad;
                }
            },

            // Add legend
            legend: {
                orient: 'vertical',
                top: 'center',
                left: 0,
                formatter: function (params, ticket, callback) {
                    return valuesInd[params];
                },
                data: dataLegend,
                itemHeight: 8,
                itemWidth: 8
            },

            // Add series
            series: [
                {
                    name: 'Indicadores',
                    type: 'funnel',
                    left: '25%',
                    right: '25%',
                    top: '16%',
                    height: '84%',
                    itemStyle: {
                        normal: {
                            borderColor: '#fff',
                            borderWidth: 1,
                            label: {
                                position: 'right'
                            }
                        }
                    },
                    data: dataSeries/*[
                        { value: 60, name: 'Safari' },
                        { value: 40, name: 'Firefox' },
                        { value: 20, name: 'Chrome' },
                        { value: 80, name: 'Opera' },
                        { value: 100, name: 'IE' }
                    ]*/
                }
            ]
        });
    }


    //
    // Resize charts
    //

    // Resize function
    var triggerChartResize = function () {
        funnel_basic_element && funnel_basic.resize();
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


var _DashboardDonutChart = function (elemento, size, data) {
    if (typeof d3 == 'undefined') {
        console.warn('Warning - d3.min.js is not loaded.');
        return;
    }

    var element = document.getElementById(elemento);
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
                return '<ul class="list-unstyled mb-1">' +
                    '<li>' + '<div class="font-size-base mb-1 mt-1">' + d.data.status + '</div>' + '</li>' +
                    '<li>' + '<span class="font-weight-semibold float-right">' + d.value + '</span>' + '</li>' +
                    '</ul>';
            });


        // Create chart
        // ------------------------------

        // Add svg element
        var container = d3Container.append('svg').call(tip);

        // Add SVG group
        var svg = container
            .attr('width', size)
            .attr('height', size)
            .style('margin', 'auto')
            .append('g')
            .attr('transform', 'translate(' + (size / 2) + ',' + (size / 2) + ')');



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
            .innerRadius(radius / 2);


        //
        // Append chart elements
        //

        // Group chart elements
        var arcGroup = svg.selectAll('.d3-arc')
            .data(pie(data))
            .enter()
            .append('g')
            .attr('class', 'd3-arc d3-slice-border')
            .style('cursor', 'pointer');

        // Append path
        var arcPath = arcGroup
            .append('path')
            .style('fill', function (d) { return d.data.color });

        // Add tooltip
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
            })

            .on('mousemove', function (d) {

                // Show tooltip on mousemove
                tip.show(d)
                    .style('top', (d3.event.pageY - 40) + 'px')
                    .style('left', (d3.event.pageX + 30) + 'px');
            })

            .on('mouseout', function (d, i) {

                // Mouseout transition
                d3.select(this)
                    .transition()
                    .duration(500)
                    .ease('bounce')
                    .attr('transform', 'translate(0,0)');

                // Hide tooltip
                tip.hide(d);
            });

        // Animate chart on load
        arcPath
            .transition()
            .delay(function (d, i) { return i * 500; })
            .duration(500)
            .attrTween('d', function (d) {
                var interpolate = d3.interpolate(d.startAngle, d.endAngle);
                return function (t) {
                    d.endAngle = interpolate(t);
                    return arc(d);
                };
            });
    }
};


   // Scatter punch chart
