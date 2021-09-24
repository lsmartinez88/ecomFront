// Simple pie
var _ratingChart = function (element, size, data) {
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


        // Tooltip
        // ------------------------------

        var tip = d3.tip()
            .attr('class', 'd3-tip')
            .offset([-10, 0])
            .direction('e')
            .html(function (d) {
                return "<ul class='list-unstyled mb-1'>" +
                    "<li>" + "<div class='font-size-base my-1'>" + d.data.icon + d.data.status + "</div>" + "</li>" +
                    "<li>" + "" + "<span class='font-weight-semibold float-left ml-3'>" + (100 / (sum / d.value)).toFixed(2) + "%" + "</span>" + "</li>" +
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
            .outerRadius(radius);


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

        // Add tooltip
        arcPath
            .on('mouseover', function (d, i) {

                // Transition on mouseover
                d3.select(this)
                    .transition()
                    .duration(1000)
                    .ease('elastic')
                    .attr('transform', function (d) {
                        d.midAngle = ((d.endAngle - d.startAngle) / 2) + d.startAngle;
                        var x = Math.sin(d.midAngle) * distance;
                        var y = -Math.cos(d.midAngle) * distance;
                        return 'translate(' + x + ',' + y + ')';
                    });
            })
            .on("mousemove", function (d) {

                // Show tooltip on mousemove
                tip.show(d)
                    .style("top", (d3.event.pageY - 40) + "px")
                    .style("left", (d3.event.pageX + 30) + "px");
            })
            .on('mouseout', function (d, i) {

                // Mouseout transition
                d3.select(this)
                    .transition()
                    .duration(3000)
                    .ease('bounce')
                    .attr('transform', 'translate(0,0)');

                // Hide tooltip
                tip.hide(d);
            });

        // Animate chart on load
        arcPath
            .transition()
            .delay(function (d, i) { return i * 500; })
            .duration(3000)
            .attrTween("d", function (d) {
                var interpolate = d3.interpolate(d.startAngle, d.endAngle);
                return function (t) {
                    d.endAngle = interpolate(t);
                    return arc(d);
                };
            });


        // Animate counter
        d3Container.select('h2')
            .transition()
            .duration(3000)
            .tween("text", function (d) {
                var i = d3.interpolate(this.textContent, sum);

                return function (t) {
                    this.textContent = d3.format(",d")(Math.round(i(t)));
                };
            });
    }
};


// Segmented gauge
var _segmentedGauge = function (element, size, min, max, sliceQty, colorMin, colorMax, data) {
    if (typeof d3 == 'undefined') {
        console.warn('Warning - d3.min.js is not loaded.');
        return;
    }

    // Initialize chart only if element exsists in the DOM
    if (element) {

        // Main variables
        var d3Container = d3.select(element),
            width = size,
            height = (size / 2) + 20,
            radius = (size / 2),
            ringInset = 15,
            ringWidth = 20,

            pointerWidth = 10,
            pointerTailLength = 5,
            pointerHeadLengthPercent = 0.75,

            minValue = min,
            maxValue = max,

            minAngle = -90,
            maxAngle = 90,

            slices = sliceQty,
            range = maxAngle - minAngle,
            pointerHeadLength = Math.round(radius * pointerHeadLengthPercent);

        // Colors
        var colors = d3.scale.linear()
            .domain([0, slices - 1])
            .interpolate(d3.interpolateHsl)
            .range([colorMin, colorMax]);


        // Create chart
        // ------------------------------

        // Add SVG element
        var container = d3Container.append('svg');

        // Add SVG group
        var svg = container
            .attr('width', width)
            .attr('height', height);


        // Construct chart layout
        // ------------------------------

        // Donut  
        var arc = d3.svg.arc()
            .innerRadius(radius - ringWidth - ringInset)
            .outerRadius(radius - ringInset)
            .startAngle(function (d, i) {
                var ratio = d * i;
                return deg2rad(minAngle + (ratio * range));
            })
            .endAngle(function (d, i) {
                var ratio = d * (i + 1);
                return deg2rad(minAngle + (ratio * range));
            });

        // Linear scale that maps domain values to a percent from 0..1
        var scale = d3.scale.linear()
            .range([0, 1])
            .domain([minValue, maxValue]);

        // Ticks
        var ticks = scale.ticks(slices);
        var tickData = d3.range(slices)
            .map(function () {
                return 1 / slices;
            });

        // Calculate angles
        function deg2rad(deg) {
            return deg * Math.PI / 180;
        }

        // Calculate rotation angle
        function newAngle(d) {
            var ratio = scale(d);
            var newAngle = minAngle + (ratio * range);
            return newAngle;
        }


        // Append chart elements
        // ------------------------------

        //
        // Append arc
        //

        // Wrap paths in separate group
        var arcs = svg.append('g')
            .attr('class', 'd3-slice-border')
            .attr('transform', "translate(" + radius + "," + radius + ")");

        // Add paths
        arcs.selectAll('path')
            .data(tickData)
            .enter()
            .append('path')
            .attr('fill', function (d, i) {
                return colors(i);
            })
            .attr('d', arc);


        //
        // Text labels
        //

        // Wrap text in separate group
        var arcLabels = svg.append('g')
            .attr('transform', "translate(" + radius + "," + radius + ")");

        // Add text
        arcLabels.selectAll('text')
            .data(ticks)
            .enter()
            .append('text')
            .attr('class', 'd3-text opacity-50')
            .attr('transform', function (d) {
                var ratio = scale(d);
                var newAngle = minAngle + (ratio * range);
                return 'rotate(' + newAngle + ') translate(0,' + (10 - radius) + ')';
            })
            .style({
                'text-anchor': 'middle',
                'font-size': 11
            })
            .text(function (d) { return d + "%"; });


        //
        // Pointer
        //

        // Line data
        var lineData = [
            [pointerWidth / 2, 0],
            [0, -pointerHeadLength],
            [-(pointerWidth / 2), 0],
            [0, pointerTailLength],
            [pointerWidth / 2, 0]
        ];

        // Create line
        var pointerLine = d3.svg.line()
            .interpolate('monotone');

        // Wrap all lines in separate group
        var pointerGroup = svg
            .append('g')
            .data([lineData])
            .attr('transform', "translate(" + radius + "," + radius + ")");

        // Paths
        pointer = pointerGroup
            .append('path')
            .attr('d', pointerLine)
            .attr('transform', 'rotate(' + minAngle + ')');


        // Random update
        // ------------------------------

        // Update values
        function update() {
            var ratio = scale(data * max);
            var newAngle = minAngle + (ratio * range);
            pointer.transition()
                .duration(3500)
                .ease('elastic')
                .attr('transform', 'rotate(' + newAngle + ')');
        }
        update();
    }
};

var _heatMapSellers = function (element, data) {
    if (typeof d3 == 'undefined') {
        console.warn('Warning - d3.min.js is not loaded.');
        return;
    }

    // Initialize chart only if element exsists in the DOM
    if (element) {

                // Bind data
            // ------------------------------

            // Nest data
            var nested_data = d3.nest().key(function (d) { return d.app; }),
                nest = nested_data.entries(data);

            // Format date
            var format = d3.time.format('%Y/%m/%d %H:%M'),
                formatTime = d3.time.format('%H:%M');

            // Pull out values
            data.forEach(function (d, i) {
                d.date = format.parse(d.date),
                    d.value = +d.value
            });



            // Layout setup
            // ------------------------------

            // Define main variables
            var d3Container = d3.select(element);
            margin = { top: 20, right: 0, bottom: 30, left: 0 },
                width = d3Container.node().getBoundingClientRect().width - margin.left - margin.right,
                gridSize = width / new Date(data[data.length - 1].date).getHours(), // dynamically set grid size
                rowGap = 40, // vertical gap between rows
                height = (rowGap + gridSize) * (d3.max(nest, function (d, i) { return i + 1 })) - margin.top,
                buckets = 5, // number of colors in range
                colors = ['#e9d8fa', '#d2b5ef', '#b990e1', '#9f6bd3', '#8548c3'];



            // Construct scales
            // ------------------------------

            // Horizontal
            var x = d3.time.scale().range([0, width]);

            // Vertical
            var y = d3.scale.linear().range([height, 0]);

            // Colors
            var colorScale = d3.scale.quantile()
                .domain([0, buckets - 1, d3.max(data, function (d) { return d.value; })])
                .range(colors);



            // Set input domains
            // ------------------------------

            // Horizontal
            x.domain([new Date(data[0].date), d3.time.hour.offset(new Date(data[data.length - 1].date), 1)]);

            // Vertical
            y.domain([0, d3.max(data, function (d) { return d.app; })]);



            // Create chart
            // ------------------------------

            // Container
            var container = d3Container.append('svg');

            // SVG element
            var svg = container
                .attr('width', width + margin.left + margin.right)
                .attr('height', height + margin.bottom)
                .append('g')
                .attr('transform', 'translate(' + margin.left + ',' + margin.top + ')');



            //
            // Append chart elements
            //

            // App groups
            // ------------------------------

            // Add groups for each app
            var hourGroup = svg.selectAll('.hour-group')
                .data(nest)
                .enter()
                .append('g')
                .attr('class', 'hour-group')
                .attr('transform', function (d, i) { return 'translate(0, ' + ((gridSize + rowGap) * i) + ')'; });

            // Add app name
            hourGroup
                .append('text')
                .attr('class', 'd3-text')
                .attr('x', 0)
                .attr('y', -(margin.top / 2))
                .text(function (d, i) { return d.key; });

            // Sales count text
            hourGroup
                .append('text')
                .attr('class', 'sales-count d3-text')
                .attr('x', width)
                .attr('y', -(margin.top / 2))
                .style('text-anchor', 'end')
                .text(function (d, i) { return d3.sum(d.values, function (d) { return d.value; }) + ' sales today' });



            // Add map elements
            // ------------------------------

            // Add map squares
            var heatMap = hourGroup.selectAll('.heatmap-hour')
                .data(function (d) { return d.values })
                .enter()
                .append('rect')
                .attr('x', function (d, i) { return x(d.date); })
                .attr('y', 0)
                .attr('class', 'heatmap-hour d3-slice-border d3-bg')
                .attr('width', gridSize)
                .attr('height', gridSize)
                .style('cursor', 'pointer');

            // Add loading transition    
            heatMap.transition()
                .duration(250)
                .delay(function (d, i) { return i * 20; })
                .style('fill', function (d) { return colorScale(d.value); })

            // Add user interaction
            hourGroup.each(function (d) {
                heatMap
                    .on('mouseover', function (d, i) {
                        d3.select(this).style('opacity', 0.75);
                        d3.select(this.parentNode).select('.sales-count').text(function (d) { return d.values[i].value + ' sales at ' + formatTime(d.values[i].date); })
                    })
                    .on('mouseout', function (d, i) {
                        d3.select(this).style('opacity', 1);
                        d3.select(this.parentNode).select('.sales-count').text(function (d, i) { return d3.sum(d.values, function (d) { return d.value; }) + ' sales today' })
                    })
            })



            // Add legend
            // ------------------------------

            // Get min and max values
            var minValue, maxValue;
            data.forEach(function (d, i) {
                maxValue = d3.max(data, function (d) { return d.value; });
                minValue = d3.min(data, function (d) { return d.value; });
            });

            // Place legend inside separate group
            var legendGroup = svg.append('g')
                .attr('class', 'legend-group')
                .attr('width', width)
                .attr('transform', 'translate(' + ((width / 2) - ((buckets * gridSize)) / 2) + ',' + (height + (margin.bottom - margin.top)) + ')');

            // Then group legend elements
            var legend = legendGroup.selectAll('.heatmap-legend')
                .data([0].concat(colorScale.quantiles()), function (d) { return d; })
                .enter()
                .append('g')
                .attr('class', 'heatmap-legend');

            // Add legend items
            legend.append('rect')
                .attr('class', 'heatmap-legend-item d3-slice-border')
                .attr('x', function (d, i) { return gridSize * i; })
                .attr('y', -8)
                .attr('width', gridSize)
                .attr('height', 5)
                .style('stroke-width', 1)
                .style('fill', function (d, i) { return colors[i]; });

            // Add min value text label
            legendGroup.append('text')
                .attr('class', 'min-legend-value d3-text')
                .attr('x', -10)
                .attr('y', -2)
                .style('text-anchor', 'end')
                .style('font-size', 11)
                .text(minValue);

            // Add max value text label
            legendGroup.append('text')
                .attr('class', 'max-legend-value d3-text')
                .attr('x', (buckets * gridSize) + 10)
                .attr('y', -2)
                .style('font-size', 11)
                .text(maxValue);



            // Resize chart
            // ------------------------------

            // Call function on window resize
            var resizeHeatmapTimer;
            window.addEventListener('resize', function () {
                clearTimeout(resizeHeatmapTimer);
                resizeHeatmapTimer = setTimeout(function () {
                    resizeHeatmap();
                }, 200);
            });

            // Call function on sidebar width change
            var sidebarToggle = document.querySelectorAll('.sidebar-control');
            if (sidebarToggle) {
                sidebarToggle.forEach(function (togglers) {
                    togglers.addEventListener('click', resizeHeatmap);
                });
            }

            // Resize function
            // 
            // Since D3 doesn't support SVG resize by default,
            // we need to manually specify parts of the graph that need to 
            // be updated on window resize
            function resizeHeatmap() {

                // Layout
                // -------------------------

                // Width
                width = d3Container.node().getBoundingClientRect().width - margin.left - margin.right,

                    // Grid size
                    gridSize = width / new Date(data[data.length - 1].date).getHours(),

                    // Height
                    height = (rowGap + gridSize) * (d3.max(nest, function (d, i) { return i + 1 })) - margin.top,

                    // Main svg width
                    container.attr('width', width + margin.left + margin.right).attr('height', height + margin.bottom);

                // Width of appended group
                svg.attr('width', width + margin.left + margin.right).attr('height', height + margin.bottom);

                // Horizontal range
                x.range([0, width]);


                // Chart elements
                // -------------------------

                // Groups for each app
                svg.selectAll('.hour-group')
                    .attr('transform', function (d, i) { return 'translate(0, ' + ((gridSize + rowGap) * i) + ')'; });

                // Map squares
                svg.selectAll('.heatmap-hour')
                    .attr('width', gridSize)
                    .attr('height', gridSize)
                    .attr('x', function (d, i) { return x(d.date); });

                // Legend group
                svg.selectAll('.legend-group')
                    .attr('transform', 'translate(' + ((width / 2) - ((buckets * gridSize)) / 2) + ',' + (height + margin.bottom - margin.top) + ')');

                // Sales count text
                svg.selectAll('.sales-count')
                    .attr('x', width);

                // Legend item
                svg.selectAll('.heatmap-legend-item')
                    .attr('width', gridSize)
                    .attr('x', function (d, i) { return gridSize * i; });

                // Max value text label
                svg.selectAll('.max-legend-value')
                    .attr('x', (buckets * gridSize) + 10);
            }
    }
};

/*
// Progress arc - single color
var _progressArcSingle = function (element, size, data) {
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
            color = '#45748a';


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
            .text(100)
            .attr({
                'text-anchor': 'middle',
                'x': (radius - thickness / 2)
            });

        // Min
        scale.append('text')
            .text(0)
            .attr({
                'text-anchor': 'middle',
                'x': -(radius - thickness / 2)
            });

            v = d3.format('.0f')(data * 100);
            foreground.transition()
                .duration(3000)
                .call(arcTween, v);

            value.transition()
                .duration(3000)
                .call(textTween, v);

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
*/