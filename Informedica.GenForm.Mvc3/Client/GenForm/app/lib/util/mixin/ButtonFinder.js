Ext.define('GenForm.lib.util.mixin.ButtonFinder', {

    _buttons: null,

    getButtons: function() {
        var me = this,
            buttons = me._buttons;

        if (!buttons) {
            buttons = me._buttons = Ext.create('Ext.util.MixedCollection');
            buttons.addAll(me.query('button'));
        }

        return buttons;
    },

    findButton: function(identifier) {
        var me = this, btn;
        
        btn = me.getButtons().findBy(function(f) {
            return f.text === identifier || f.itemId === identifier;
        });

        return btn;
    }
});