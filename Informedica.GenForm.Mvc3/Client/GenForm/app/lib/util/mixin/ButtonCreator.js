Ext.define('GenForm.lib.util.mixin.ButtonCreator', {

    createButton: function (config) {
        var me = this;

        if (!me.controls) me.controls = {};
        if (!me.controls.buttons) me.controls.buttons = {};

        me.controls.buttons[config.action] = Ext.create('Ext.button.Button', config);
        return me.controls.buttons[config.action];
    }

});