Ext.define('GenForm.controller.mixin.ShapeHandler', {

    addShapeToStore: function (shape) {
        var me = this,
            store = me.getShapeStore();

        store.add({ShapeName: shape});
    },

    createEmptyShape: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.ShapeName');
    },

    createShapeWindow: function () {
        return Ext.create(this.getProductShapeWindowView());
    },

    editOrAddShape: function () {
        var me = this;
        me.getShapeWindow().show();
    },

    getShape: function (button) {
        return button.up('panel').down('form').getShape();
    },

    getShapeStore: function () {
        var me = this;
        return me.getProductShapeNameStore();
    },

    loadEmptyShape: function (window) {
        window.loadWithShape(this.createEmptyShape());
    },

    getShapeWindow: function () {
        var me = this, form;

        form = me.createShapeWindow();
        me.loadEmptyShape(form);
        return form;
    },

    onShapeSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('shapewindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Shape saved: ', result.data.ShapeName);
            me.addShapeToStore(result.data.ShapeName);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Shape could not be saved: ', result.message);
        }
    },

    saveShape: function (button) {
        var me = this,
            shape = me.getShape(button);

        Product.AddNewShape(shape.data, {scope: me, callback:me.onShapeSaved});
    }
});