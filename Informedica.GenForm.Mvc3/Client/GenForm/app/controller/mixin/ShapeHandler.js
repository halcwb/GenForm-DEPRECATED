Ext.define('GenForm.controller.mixin.ShapeHandler', {

    addShapeToStore: function (shape) {
        var me = this,
            store = me.getShapeStore();

        store.add(shape);
    },

    createEmptyShape: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.Shape');
    },

    createShapeWindow: function () {
        return Ext.create(this.getProductShapeWindowView());
    },

    onAddShape: function () {
        var me = this,
            window = me.getShapeWindow(me.createEmptyShape()).show();

        window.setTitle('Nieuwe artikel vorm');
        window.show();
    },

    onEditShape: function (button) {
        var me = this,
            form = button.findParentByType('productform'),
            shape = form.fields.Shape.findRecord('Name', form.fields.Shape.getValue()),
            window = me.getShapeWindow(shape);

        window.show();
    },

    getShape: function (button) {
        return button.up('panel').down('form').getShape();
    },

    getShapeStore: function () {
        var me = this;
        return me.getProductShapeStore();
    },

    loadEmptyShape: function (window) {
        window.loadWithShape(this.createEmptyShape());
    },

    getShapeWindow: function (shape) {
        var me = this, window;

        window = me.createShapeWindow();
        if (!shape) {
            me.loadEmptyShape(window);
        } else {
            window.setTitle('Bewerk artikel vorm: ' + shape.data.Name);
            window.loadWithShape(shape);
        }

        return window;
    },

    onShapeSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('shapewindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Shape saved: ', result.data.Name);
            me.addShapeToStore(result.data);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Shape could not be saved: ', result.message);
        }
    },

    onSaveShape: function (button) {
        var me = this,
            shape = me.getShape(button);

        GenForm.server.UnitTest.SaveShape(shape.data, {scope: me, callback:me.onShapeSaved});
    }
});