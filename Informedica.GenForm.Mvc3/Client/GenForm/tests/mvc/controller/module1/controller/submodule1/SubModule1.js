Ext.define('TestApplication.controller.module1.controller.submodule1.SubModule1', {
   extend: 'Ext.app.Application',

    appFolder: '../Client/GenForm/tests/mvc/controller/module1/controller/submodule1',

    constructor: function (config) {
        var me = this;
        me.callParent(arguments);
    },

    init: function () {
        var me = this;
        me.callParent(arguments);
    },

    launch: function () {
        var me = this;
        console.log('launch submodule1');
    }

});