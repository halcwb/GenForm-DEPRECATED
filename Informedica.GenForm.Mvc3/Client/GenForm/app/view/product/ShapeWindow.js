Ext.define('GenForm.view.product.ShapeWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.shapewindow',

    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        var me = this;
        me.forms = {};
        me.items = me.createShapeForm();

        me.callParent(arguments);
    },

    createShapeForm: function () {
        var me = this;
        return me.createForm({ xtype: 'shapeform', name: 'ShapeForm' });
    },

    loadWithShape: function (shape) {
        var me = this;
        me.forms.ShapeForm.loadRecord(shape);
    }
});