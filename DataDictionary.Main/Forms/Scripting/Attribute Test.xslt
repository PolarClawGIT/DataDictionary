<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    exclude-result-prefixes="msxsl" >
  <xsl:output method="text"/>
  <xsl:template match="/Model.Attribute">
    <xsl:value-of select="AttributeTitle" /><xsl:text>: </xsl:text><xsl:value-of select="AttributeDescription" />
  </xsl:template>
</xsl:stylesheet>
