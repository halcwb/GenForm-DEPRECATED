Ext.define('GenForm.lib.util.mixin.ButtonCreator', {
    createButton: function (config) {
        var me = this;

        me.buttons[config.action] = Ext.create('Ext.button.Button', config);
        return me.buttons[config.action];
    }

});