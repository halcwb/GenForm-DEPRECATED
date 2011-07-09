/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/15/11
 * Time: 2:48 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.application({
    name: 'TestApplication',

    appFolder: '../Client/GenForm/tests/mvc',

    models: [
    ],

    controllers: [
        'module1.Module1',
        'module1.TestController'
    ],

    launch: function () {
        var me = this;
        GenForm.tests = me;
    }

});