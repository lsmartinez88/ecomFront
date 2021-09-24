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