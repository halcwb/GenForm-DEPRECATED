Ext.define('GenForm.view.product.ShapeWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.shapewindow',

    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        this.items = this.createShapeForm();

        this.callParent(arguments);
    },

    createShapeForm: function () {
        return { xtype: 'shapeform' };
    },

    getShapeForm: function () {
        return this.items.items[0];
    },

    loadWithShape: function (shape) {
        this.getShapeForm().getForm().loadRecord(shape);
    }

});