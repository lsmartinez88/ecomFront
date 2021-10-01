var _homeCompetidores = function (elemento, data) {
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
            radius = 80
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
            .attr('width', 170)
            .attr('height', 85)
            .append('g')
            .attr('transform', 'translate(' + (85) + ',' + (85) + ')');



        // Construct chart layout
        // ------------------------------

        // Pie
        var pie = d3.layout.pie()
            .sort(null)
            .startAngle(-Math.PI / 2)
            .endAngle(Math.PI / 2)
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
                    .style('top', (d3.event.pageY - 50) + 'px')
                    .style('left', (d3.event.pageX + 40) + 'px');
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
                return d.data.text + ': ';
            });

        // Add value
        legend.append('span')
            .text(function (d, i) {
                return d.data.value;
            });

        // Animate chart on load
        arcPath
            .transition()
            .duration(500)
            .delay(function (d, i) { return i * 500; })
            .attrTween('d', function (d) {
                var interpolate = d3.interpolate(d.startAngle, d.endAngle);
                return function (t) {
                    d.endAngle = interpolate(t);
                    return arc(d);
                };
            });
    }
};
