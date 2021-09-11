
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

//colors: ['#2196F3', '#f35c86', '#25b372', '#f58646', '#ef5350', '#009688', '#3F51B5', '#45748a', '#8e70c1'],